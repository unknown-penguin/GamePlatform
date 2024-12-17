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

            var user = new User { UserName = model.UserName, Email = model.Email };
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
                return Ok(new { message = "User logged in successfully.", token = jwt_token });
            }

            if (result.IsLockedOut)
            {
                return BadRequest("User account locked out.");
            }

            return BadRequest(new { message = "Invalid login attempt.", errors = $"{result}" });
        }
        
    }
}