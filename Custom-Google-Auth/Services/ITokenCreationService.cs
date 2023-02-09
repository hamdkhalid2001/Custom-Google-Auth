using Custom_Google_Auth.Authentication;
using Custom_Google_Auth.Models;

namespace Custom_Google_Auth.Services;
public interface ITokenCreationService
{
    AuthenticationResponse CreateToken(Users user);

}