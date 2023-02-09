using System.ComponentModel.DataAnnotations;

namespace Custom_Google_Auth.Authentication
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; } 
        [Required]
        public string PasswordHash { get; set; }
    }
}
