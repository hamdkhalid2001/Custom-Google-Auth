using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Custom_Google_Auth.Models
{
    public class Users: IdentityUser
    {
        public int? Age { get; set; }
    }
}
