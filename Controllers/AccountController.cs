using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
         private UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register(dtoNewsUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            AppUser newUser = new()
             {
                    FullName=user.Name,
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
              };
                var result = _userManager.CreateAsync(newUser, user.Password).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);

            }
            await _userManager.AddToRoleAsync(newUser, "User");
            return Ok("Account created successfully");

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return BadRequest("Email and password are required");
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found.");
            var isValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isValid)
                return Unauthorized("Invalid credentials.");

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                email = user.Email
            }); ;
        }

    }
}
