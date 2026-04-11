using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
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



        }
}
