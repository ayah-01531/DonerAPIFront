using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        public DonationController(AppDbContext db)
        {
            _db = db;

        }
        private readonly AppDbContext _db;
        [HttpGet]
        public async Task<IActionResult> Getdonation()
        {
            var data = await _db.Donations.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getdoner(int id)
        {
            var data = await _db.Donations.SingleOrDefaultAsync(x => x.DonationId == id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Adddonation([FromForm] Donation donation)
        {
            donation.RegistrationDate = DateTime.Now;
            
          
            await _db.Donations.AddAsync(donation);
            await _db.SaveChangesAsync();

            return Ok(donation);
        }
        [HttpPut]
        public async Task<IActionResult> Updatedoner(int donationid, string name, string bloadtype, string phonenumber,string adress, string nationalidnumber, string organtype)
        {
            var data = await _db.Donations.FindAsync(donationid);
            if (data == null)
            {
                return NotFound($"Doner Id {donationid} not exists");
            }
            using var stream= new MemoryStream();
            data.PatientName = name;
            data.Address = adress;
            data.NationalIDNumber = nationalidnumber;
            data.PhoneNumber = phonenumber;
            data.BloodType = bloadtype;
            data.OrganType = organtype;
            _db.Donations.Update(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatedonationPatch([FromBody] JsonPatchDocument<Donation> donation, [FromRoute] int id)
        {
            var data = await _db.Donations.SingleOrDefaultAsync(x => x.DonationId == id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            donation.ApplyTo(data, ModelState);
            await _db.SaveChangesAsync();
            return Ok(data);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Deletedonation(int id)
        {
            var data = await _db.Donations.FindAsync(id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            _db.Donations.Remove(data);
            _db.SaveChanges();
            return Ok($"Doner Id {id} is deleted");
        }
       
    }
}
