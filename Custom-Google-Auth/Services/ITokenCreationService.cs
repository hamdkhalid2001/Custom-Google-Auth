using Custom_Google_Auth.Models;

namespace Custom_Google_Auth.Services;
public interface ITokenCreationService
{
    object CreateToken(Users user, string role);

}