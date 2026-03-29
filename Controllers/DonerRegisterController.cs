using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonerRegisterController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<DonerRegister> _passwordHasher;
        public DonerRegisterController(AppDbContext db)
        {
            _db = db;
            _passwordHasher = new PasswordHasher<DonerRegister>();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DonerRegister donerRegister)  {
            if(_db.DonerRegisters.Any(h => h.Email == donerRegister.Email))
            {
                return BadRequest("Email already in use.");
            }
            donerRegister.Password = _passwordHasher.HashPassword(donerRegister, donerRegister.Password);
            await _db.DonerRegisters.AddAsync(donerRegister);
            await _db.SaveChangesAsync();
            return Ok("Doner registered successfully.");


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var Doner = _db.DonerRegisters.FirstOrDefault(h => h.Email == email);
            if( Doner == null)
            {
                return NotFound("Hospital not found.");
            }
            var result = _passwordHasher.VerifyHashedPassword(Doner, Doner.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok("Login successful.");
        }
    }
}
