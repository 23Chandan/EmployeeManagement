using EmployeeManagement.Core.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        [HttpGet("roleList")]
        public async Task<IActionResult> RoleList()
        {
            var role = await _roleManager.Roles.ToArrayAsync();
            return Ok(role);
        }
        //[Authorize(Roles ="Admin")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterApplicationUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.Role))
                {
                    var roleExists = await _roleManager.RoleExistsAsync(model.Role);
                    if (!roleExists)
                    {
                        return BadRequest(new { message = "Role does not exist" });
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                return Ok(new { message = "User registered successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LogInApplicationUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains(model.Role))
                {
                    return Ok(new { message = "Invalid Role Selected" });
                }
                var token = GenerateJwtToken(model.Email, roles);
                return Ok(new { message = "Login successful", token = token });
            }
            else
            {
                return Ok(new { message = " UserName or password incorrect"});
            }
        }
        private string GenerateJwtToken(string username, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
