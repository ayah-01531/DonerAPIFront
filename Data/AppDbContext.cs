using Hope_for_Organ_Donation.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Hope_for_Organ_Donation.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Doner> Doners { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonerRegister> DonerRegisters { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configure Hospital table relationships and unique constraints

            builder.Entity<Hospital>().HasOne(h => h.User).WithOne(u => u.Hospital).HasForeignKey<Hospital>(h => h.UserId);
            builder.Entity<Hospital>().HasIndex(h => h.LicenseNumber).IsUnique();
            builder.Entity<Hospital>().HasIndex(h => h.UserId).IsUnique();

            // Configure Doner table relationships and unique constraints

            builder.Entity<Doner>().HasOne(d => d.User).WithOne(u => u.Donor).HasForeignKey<Doner>(d => d.UserId);
            builder.Entity<Doner>().HasIndex(d => d.UserId).IsUnique();

            // Configure Donation table relationships and unique constraints

            builder.Entity<Donation>().HasOne(dr => dr.Hospital).WithMany(h => h.Donation).HasForeignKey(dr => dr.HospitalId).IsRequired();

       
       
       
        }




        }
    }
