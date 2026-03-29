using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<Hospital> _passwordHasher;
        public HospitalController(AppDbContext db)
        {
            _db = db;
            _passwordHasher = new PasswordHasher<Hospital>();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Hospital hospital)
        {
            if (_db.Hospitals.Any(h => h.Email == hospital.Email))
            {
                return BadRequest("Email already in use.");
            }
            hospital.Password = _passwordHasher.HashPassword(hospital, hospital.Password);
            await _db.Hospitals.AddAsync(hospital);
            await _db.SaveChangesAsync();
            return Ok("Hospital registered successfully.");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hospital = _db.Hospitals.FirstOrDefault(h => h.Email == email);
            if (hospital == null)
            {
                return NotFound("Hospital not found.");
            }
            var result = _passwordHasher.VerifyHashedPassword(hospital, hospital.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok("Login successful.");
        }

    }
}
