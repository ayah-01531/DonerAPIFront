using Microsoft.EntityFrameworkCore;
using Hope_for_Organ_Donation.Model;
namespace Hope_for_Organ_Donation.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Doner> Doners { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<DonerRegister> DonerRegisters { get; set; }




    }
}
    
