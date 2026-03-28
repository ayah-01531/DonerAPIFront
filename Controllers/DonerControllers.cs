using Hope_for_Organ_Donation.Data;
using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;




namespace Hope_for_Organ_Donation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonerControllers : ControllerBase
    {
        public DonerControllers(AppDbContext db)
        {
            _db = db;

        }
        private readonly AppDbContext _db;
        [HttpGet]
        public async Task<IActionResult> Getdoner()
        {
            var data = await _db.Doners.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getdoner(int id)
        {
            var data = await _db.Doners.SingleOrDefaultAsync(x => x.DonerId == id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Adddoner([FromBody] Doner doner)
        {
            doner.RegistrationDate = DateTime.Now;

            await _db.Doners.AddAsync(doner);
            await _db.SaveChangesAsync();

            return Ok(doner);
        }
        [HttpPut]
        public async Task<IActionResult> Updatedoner(int id, string name, string bloadtype, int age, string organtype)
        {
            var data = await _db.Doners.FindAsync(id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            data.Name = name;
            data.BloodType = bloadtype;
            data.OrganType = organtype;
            _db.Doners.Update(data);
            _db.SaveChanges();
            return Ok(data);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatedonerPatch([FromBody] JsonPatchDocument<Doner> doner, [FromRoute] int id)
        {
            var data = await _db.Doners.SingleOrDefaultAsync(x => x.DonerId == id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            doner.ApplyTo(data, ModelState);
            await _db.SaveChangesAsync();
            return Ok(data);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Deletedoner(int id)
        {
            var data = await _db.Doners.FindAsync(id);
            if (data == null)
            {
                return NotFound($"Doner Id {id} not exists");
            }
            _db.Doners.Remove(data);
            _db.SaveChanges();
            return Ok($"Doner Id {id} is deleted");
        }
    }
}
