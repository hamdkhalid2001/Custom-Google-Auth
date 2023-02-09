using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Custom_Google_Auth.Models
{
    public class Users: IdentityUser
    {
        //[Required]
        //public string UserName { get; set; }
        //[Required]
        //public string Password { get; set; }
        //[Required]
        //public string Email { get; set; }
        public int? Age { get; set; }
    }
}
