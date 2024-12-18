using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;
using authentication_service.Models;
using AuthenticationService.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AuthenticationService.Services;
using System.Text.Json;
using System.Net.Http.Headers;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationProducerService _producer;

        public AuthenticationController(UserManager<User> userManager,
                                        SignInManager<User> signInManager,
                                        IConfiguration configuration,
                                        AuthenticationProducerService producerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _producer = producerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Authorization service is running.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var jwt_token = GenerateJWTToken(user);
                await _producer.ProduceMessageAsync("AuthenticationTopic", user.UserName);
                return Ok(new { message = "User created successfully.", token = jwt_token });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("message", error.Description);
            }

            return BadRequest(ModelState);
        }

        private string GenerateJWTToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var jwt_token = GenerateJWTToken(user);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(30)
                };
                Response.Cookies.Append("jwt", jwt_token, cookieOptions);
                return Ok(new { message = "User logged in successfully." });
            }

            if (result.IsLockedOut)
            {
                return BadRequest("User account locked out.");
            }

            return BadRequest(new { message = "Invalid login attempt.", errors = $"{result}" });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginModel model)
        {
            Console.WriteLine($"Code: {model.Code}");
            var tokenResponse = await ExchangeCodeForTokenAsync(model.Code);
            var userInfo = await GetGoogleUserInfoAsync(tokenResponse.AccessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
            {
                user = new User
                {
                    UserName = userInfo.Email,
                    Email = userInfo.Email,
                    Nickname = userInfo.Name // Assuming you have a Nickname property
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            var jwtToken = GenerateJWTToken(user);
            return Ok(new { token = jwtToken });
        }

        private async Task<TokenResponse> ExchangeCodeForTokenAsync(string code)
        {
            var tokenRequestUri = "https://oauth2.googleapis.com/token";
            var values = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", _configuration["GoogleAuthSettings:ClientId"] },
            { "client_secret", _configuration["GoogleAuthSettings:ClientSecret"] },
            { "redirect_uri", "http://localhost:4200/signin-google" },
            { "grant_type", "authorization_code" }
        };
            Console.WriteLine($"values: {values}");
            using var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(values);
            var response = await httpClient.PostAsync(tokenRequestUri, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<TokenResponse>(responseString);
            }
            else
            {
                throw new Exception($"Error exchanging code for token: {responseString}");
            }
        }

        private async Task<GoogleUserInfo> GetGoogleUserInfoAsync(string accessToken)
        {
            var userInfoRequestUri = "https://www.googleapis.com/oauth2/v2/userinfo";
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync(userInfoRequestUri);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<GoogleUserInfo>(responseString);
            }
            else
            {
                throw new Exception($"Error getting user info: {responseString}");
            }
        }
    }
}