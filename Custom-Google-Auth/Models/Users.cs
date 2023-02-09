using Microsoft.AspNetCore.Identity;

namespace Custom_Google_Auth.Models
{
    public class Users: IdentityUser
    {
        public int? Age { get; set; }
    }
}
