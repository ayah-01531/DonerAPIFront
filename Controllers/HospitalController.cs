using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        private readonly UserManager<AppUser> _userManager;
        public HospitalController(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;

            _userManager = userManager;
        }
        [Authorize(Roles = "Hospital")]
        [HttpPost("Register-Hospital")]
        public async Task<IActionResult> RegisterHospital(RegisterHospitalDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            AppUser newUser = new()
            {
                UserName = user.HospitalName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.HospitalName
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(newUser, "Hospital");

            var hospital = new Hospital
            {
                HospitalName = user.HospitalName,
                LicenseNumber = user.LicenseNumber,
                Address = user.Address,
                City = user.City,
                UserId = newUser.Id
            };

            _dbContext.Hospitals.Add(hospital);
            await _dbContext.SaveChangesAsync();

            return Ok("Hospital account created successfully");
        }
        [Authorize(Roles = "Hospital")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterHospitalDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return Unauthorized("Invalid credentials");

            return Ok("Login successful.");
        }



    }
}
