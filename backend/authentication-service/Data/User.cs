using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Data
{
    public class User : IdentityUser
    {
        [Required]
        public string? Nickname { get; set; }

    }
}