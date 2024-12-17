using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace AuthorizationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthorizationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Authorization service is running.");
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            // Add your authorization logic here
            return Ok("Authorization request received.");
        }
    }
}