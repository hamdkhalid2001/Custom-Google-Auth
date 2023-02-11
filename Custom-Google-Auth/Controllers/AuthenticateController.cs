using Azure;
using Custom_Google_Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Custom_Google_Auth.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

    //    [HttpPost]
    //    [Route("login")]
    //    public async Task<IActionResult> Login([FromBody] LoginModel model)
    //    {
    //        var user = await _userManager.FindByNameAsync(model.UserName);
    //        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    //        {
    //            var userRoles = await _userManager.GetRolesAsync(user);

    //            var authClaims = new List<Claim>
    //            {
    //                new Claim(ClaimTypes.Name, user.UserName),
    //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //            };

    //            foreach (var userRole in userRoles)
    //            {
    //                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
    //            }

    //            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

    //            var token = new JwtSecurityToken(
    //                issuer: _configuration["JWT:ValidIssuer"],
    //                audience: _configuration["JWT:ValidAudience"],
    //                expires: DateTime.Now.AddHours(3),
    //                claims: authClaims,
    //                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
    //                );

    //            return Ok(new
    //            {
    //                token = new JwtSecurityTokenHandler().WriteToken(token),
    //                expiration = token.ValidTo
    //            });
    //        }
    //        return Unauthorized();
    //    }
    //    [HttpPost]
    //    [Route("register")]
    //    public async Task<IActionResult> Register([FromBody] SignupModel model)
    //    {
    //        var userExists = await _userManager.FindByNameAsync(model.UserName);
    //        if (userExists != null)
    //            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

    //        Users user = new Users()
    //        {
    //            Email = model.Email,
    //            SecurityStamp = Guid.NewGuid().ToString(),
    //            UserName = model.UserName
    //        };
    //        var result = await _userManager.CreateAsync(user, model.Password);
    //        if (!result.Succeeded)
    //            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

    //        return Ok(new { Status = "Success", Message = "User created successfully!" });
    //    }
    //    [HttpPost]
    //    [Route("register-admin")]
    //    public async Task<IActionResult> RegisterAdmin([FromBody] SignupModel model)
    //    {
    //        var userExists = await _userManager.FindByNameAsync(model.UserName);
    //        if (userExists != null)
    //            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

    //        Users user = new Users()
    //        {
    //            Email = model.Email,
    //            SecurityStamp = Guid.NewGuid().ToString(),
    //            UserName = model.UserName
    //        };
    //        var result = await _userManager.CreateAsync(user, model.Password);
    //        if (!result.Succeeded)
    //            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

    //        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
    //            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    //        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
    //            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

    //        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
    //        {
    //            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
    //        }

    //        return Ok(new { Status = "Success", Message = "User created successfully!" });
    //    }
    }
}
