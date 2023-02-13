using Azure;
using Custom_Google_Auth.Models;
using Custom_Google_Auth.Models.Authentication;
using Custom_Google_Auth.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Custom_Google_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenCreationService _jwtService;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<Users> userManager, ITokenCreationService jwtService, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        // POST: api/Users
        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult<Users>> Signup(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            var user = new Users() { UserName = model.UserName, Email = model.Email, Age = model.Age };
            
            //Role shouldn't be other than User/Admin
            if(await _roleManager.RoleExistsAsync(model.Role))
            {
                    var result = await _userManager.CreateAsync(
                    user,
                    user.PasswordHash = model.Password
                    );
                    if (!result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                    }
                    //string role;
                    //if(model.Role == null)
                    //{
                    //    role = "User";
                    //}
                    //else
                    //{
                    //    role = "Admin";
                    //}
                    result = await _userManager.AddToRoleAsync(user, "User");
                    
                    var token = _jwtService.CreateToken(user, "User");

                    user.PasswordHash = null;
                    return Created("", new { token, user });
                }
            else
            {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Invalid Role! ", Role = "role" });
            }

        }
        [HttpPost()]
        [Route("AuthenticateWithGoogle")]
        public async Task<ActionResult> AuthenticateWithGoogle(string token)
        {
            try
            {
                if (token == null)
                {
                    return BadRequest();
                }
                var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { "99100766691-r1u3hcr886oqtvl8t2i7curc94ccikmu.apps.googleusercontent.com" }
                });
                if(googleUser != null)
                {
                    var userExists = await _userManager.FindByNameAsync(googleUser.Email);
                    
                    if (userExists == null)
                    {
                        //Create User
                        Users user = new Users() {UserName = googleUser.Email , Email = googleUser.Email};
                        
                        var result = await _userManager.CreateAsync(user);
                        //Add user role
                        result = await _userManager.AddToRoleAsync(user, "User");
                        if (!result.Succeeded)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                        }
                        var jwtToken = _jwtService.CreateToken(user, "User");
                        return Created("" , new { jwtToken , user });

                    }
                    else
                    {
                        var jwtToken = _jwtService.CreateToken(userExists, "User");
                        return Ok(new { jwtToken, userExists });
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception)
            {

                throw;
            }
           
            
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var token = _jwtService.CreateToken(user, userRole[0]);
                return Ok(new { token, user ,userRole});               
            }
            return Unauthorized();
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
    }
}
