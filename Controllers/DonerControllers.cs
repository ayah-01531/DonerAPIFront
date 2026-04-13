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
    public class DonorController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;

        public DonorController(UserManager<AppUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // ✅ تسجيل Donor جديد
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDonorDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("Email already exists");

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Donor");

            var donor = new Doner
            {
                UserId = user.Id,
     
            };

            _db.Doners.Add(donor);
            await _db.SaveChangesAsync();

            return Ok("Account created successfully");
        }
        [Authorize(Roles = "Donor")]
        [HttpPut("complete-profile")]
        public async Task<IActionResult> CompleteProfile(UpdateDonorProfileDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var donor = await _db.Doners.FirstOrDefaultAsync(d => d.UserId == userId);
            if (donor == null) return NotFound();

            donor.BloodType = model.BloodType;
            donor.Address = model.Address;
            donor.NationalIDNumber = model.NationalIDNumber;
            donor.SelectedOrgans = model.SelectedOrgans;
            donor.BirthDate = model.BirthDate;
            donor.HasSignedConsent = model.HasSignedConsent.Value;

            await _db.SaveChangesAsync();
            return Ok("Profile completed successfully");
        }

        // ✅ عرض بيانات الـ Donor الحالي
        [Authorize(Roles = "Donor")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var donor = await _db.Doners
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (donor == null) return NotFound();

            return Ok(new
            {
                donor.DonerId,
                donor.User.FullName,
                donor.User.Email,
                donor.User.PhoneNumber,
                donor.BloodType,
                donor.Address,
                donor.NationalIDNumber
            });
        }

        // ✅ تعديل بيانات الـ Donor
        [Authorize(Roles = "Donor")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile(UpdateDonorDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var donor = await _db.Doners
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (donor == null) return NotFound();

            if (model.BloodType != null) donor.BloodType = model.BloodType;
            if (model.Address != null) donor.Address = model.Address;
            if (model.BirthDate!= null) donor.Address = model.BirthDate;
            if (model.SelectedOrgans != null) donor.Address = model.SelectedOrgans;
            if (model.phoneNumber != null) donor.Address = model.phoneNumber;


            await _db.SaveChangesAsync();
            return Ok("Updated successfully");
        }

        // ✅ إنشاء طلب تبرع
        [Authorize(Roles = "Donor")]
        [HttpPost("donate")]
        public async Task<IActionResult> CreateDonation(CreateDonationDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var donor = await _db.Doners
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (donor == null) return NotFound();

            var donation = new Donation
            {
                DonorId = donor.DonerId,
                OrganType = model.OrganType,
                Status = "Pending"
            };

            _db.Donations.Add(donation);
            await _db.SaveChangesAsync();

            return Ok("Donation request submitted successfully");
        }

        // ✅ عرض تبرعات الـ Donor الحالي
        [Authorize(Roles = "Donor")]
        [HttpGet("my-donations")]
        public async Task<IActionResult> GetMyDonations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var donor = await _db.Doners
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (donor == null) return NotFound();

            var donations = await _db.Donations
                .Where(d => d.DonorId == donor.DonerId)
                .Select(d => new
                {
                    d.Id,
                    d.OrganType,
                    d.Status,
                    d.CreatedAt
                })
                .ToListAsync();

            return Ok(donations);
        }

        // ✅ إلغاء طلب تبرع (فقط لو لسا Pending)
        [Authorize(Roles = "Donor")]
        [HttpDelete("cancel-donation/{id}")]
        public async Task<IActionResult> CancelDonation(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var donor = await _db.Doners
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (donor == null) return NotFound();

            var donation = await _db.Donations
                .FirstOrDefaultAsync(d => d.Id == id && d.DonorId == donor.DonerId);

            if (donation == null) return NotFound("Donation not found");

            if (donation.Status != "Pending")
                return BadRequest("Cannot cancel a donation that is already matched or completed");

            _db.Donations.Remove(donation);
            await _db.SaveChangesAsync();

            return Ok("Donation cancelled successfully");
        }
        // ✅ رفع الملف الطبي
        [Authorize(Roles = "Donor")]
        [HttpPost("upload-medical-report")]
        public async Task<IActionResult> UploadMedicalReport(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var donor = await _db.Doners.FirstOrDefaultAsync(d => d.UserId == userId);
            if (donor == null) return NotFound();

            // حفظ الملف في فولدر wwwroot/medical-reports
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "medical-reports");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{donor.DonerId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            donor.MedicalReportUrl = $"/medical-reports/{fileName}";
            await _db.SaveChangesAsync();

            return Ok(new { url = donor.MedicalReportUrl });
        }

        // ✅ تحديث بروفايل التبرع الكامل (أعضاء + توقيع + تفعيل)
        [Authorize(Roles = "Donor")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateDonorProfile(UpdateDonorProfileDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var donor = await _db.Doners.FirstOrDefaultAsync(d => d.UserId == userId);
            if (donor == null) return NotFound();

            if (model.BloodType != null) donor.BloodType = model.BloodType;
            if (model.Address != null) donor.Address = model.Address;
            if (model.SelectedOrgans != null) donor.SelectedOrgans = model.SelectedOrgans;
            if (model.HasSignedConsent.HasValue) donor.HasSignedConsent = model.HasSignedConsent.Value;

            // التفعيل يشترط التوقيع + وجود أعضاء مختارة + ملف طبي
            if (model.IsDonationActive.HasValue)
            {
                if (model.IsDonationActive.Value)
                {
                    if (!donor.HasSignedConsent)
                        return BadRequest("Must sign consent before activating donation");

                    if (string.IsNullOrEmpty(donor.SelectedOrgans))
                        return BadRequest("Must select organs before activating donation");

                    if (string.IsNullOrEmpty(donor.MedicalReportUrl))
                        return BadRequest("Must upload medical report before activating donation");
                }
                donor.IsDonationActive = model.IsDonationActive.Value;
            }

            await _db.SaveChangesAsync();
            return Ok("Profile updated successfully");
        }
    }
}