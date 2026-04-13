using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.DTOs;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;

        public HospitalController(UserManager<AppUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // ✅ تسجيل مستشفى جديد
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterHospitalDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("Email already exists");

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.HospitalName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Hospital");

            var hospital = new Hospital
            {
                UserId = user.Id,
                HospitalName = model.HospitalName,
                LicenseNumber = model.LicenseNumber,
                Address = model.Address,
                City = model.City
            };

            _db.Hospitals.Add(hospital);
            await _db.SaveChangesAsync();

            return Ok("Hospital registered successfully");
        }

        // ✅ إضافة مريض محتاج عضو
        [Authorize(Roles = "Hospital")]
        [HttpPost("add-recipient")]
        public async Task<IActionResult> AddRecipient(CreateRecipientDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var hospital = await _db.Hospitals
                .FirstOrDefaultAsync(h => h.UserId == userId);

            if (hospital == null) return NotFound("Hospital not found");

            var recipient = new Recipient
            {
                HospitalId = hospital.Id,
                FullName = model.FullName,
                BloodType = model.BloodType,
                NeededOrgan = model.NeededOrgan,
                Status = "Waiting"
            };

            _db.Recipients.Add(recipient);
            await _db.SaveChangesAsync();

            return Ok("Recipient added successfully");
        }

        // ✅ عرض مرضى المستشفى
        [Authorize(Roles = "Hospital")]
        [HttpGet("my-recipients")]
        public async Task<IActionResult> GetMyRecipients()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var hospital = await _db.Hospitals
                .FirstOrDefaultAsync(h => h.UserId == userId);

            if (hospital == null) return NotFound();

            var recipients = await _db.Recipients
                .Where(r => r.HospitalId == hospital.Id)
                .Select(r => new
                {
                    r.Id,
                    r.FullName,
                    r.BloodType,
                    r.NeededOrgan,
                    r.Status,
                    r.RegisteredAt
                })
                .ToListAsync();

            return Ok(recipients);
        }

        // ✅ عرض التبرعات المتاحة المطابقة لمرضى المستشفى
        [Authorize(Roles = "Hospital")]
        [HttpGet("available-donations")]
        public async Task<IActionResult> GetAvailableDonations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var hospital = await _db.Hospitals
                .Include(h => h.Recipients)
                .FirstOrDefaultAsync(h => h.UserId == userId);

            if (hospital == null) return NotFound();

            var neededOrgans = hospital.Recipients
                .Where(r => r.Status == "Waiting")
                .Select(r => r.NeededOrgan)
                .ToList();

            var donations = await _db.Donations
                .Where(d => d.Status == "Pending" && neededOrgans.Contains(d.OrganType))
                .Include(d => d.Donor).ThenInclude(d => d.User)
                .Select(d => new
                {
                    d.Id,
                    d.OrganType,
                    d.Status,
                    d.CreatedAt,
                    DonorName = d.Donor.User.FullName,
                    DonorBloodType = d.Donor.BloodType
                })
                .ToListAsync();

            return Ok(donations);
        }

        // ✅ مطابقة تبرع مع مريض
        [Authorize(Roles = "Hospital")]
        [HttpPut("match/{donationId}/{recipientId}")]
        public async Task<IActionResult> MatchDonation(int donationId, int recipientId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var hospital = await _db.Hospitals
                .FirstOrDefaultAsync(h => h.UserId == userId);

            if (hospital == null) return NotFound();

            var donation = await _db.Donations.FindAsync(donationId);
            if (donation == null || donation.Status != "Pending")
                return BadRequest("Donation not available");

            var recipient = await _db.Recipients.FindAsync(recipientId);
            if (recipient == null || recipient.Status != "Waiting")
                return BadRequest("Recipient not available");

            if (recipient.HospitalId != hospital.Id)
                return Forbid();

            if (donation.OrganType != recipient.NeededOrgan)
                return BadRequest("Organ type mismatch");

            donation.Status = "Matched";
            donation.RecipientId = recipientId;
            recipient.Status = "Matched";

            await _db.SaveChangesAsync();
            return Ok("Match successful");
        }
    }
}