using Custom_Google_Auth.Authentication;
using Custom_Google_Auth.Models;
using Custom_Google_Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Custom_Google_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly ITokenCreationService _jwtService;

        public UsersController(UserManager<Users> userManager, ITokenCreationService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new Users() { UserName = user.UserName, Email = user.Email, Age = user.Age },
                user.PasswordHash
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.PasswordHash = null;
            return Created("", user);
        }
        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<Users>> GetUser(string username)
        {
            Users user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return new Users
            {
                UserName = user.UserName,
                Email = user.Email,
                Age = user.Age
            };
        }
        // POST: api/Users/BearerToken
        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.PasswordHash);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            return Ok(token);
        }
    }
}
