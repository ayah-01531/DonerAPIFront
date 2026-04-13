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
        public DbSet<Recipient> Recipients { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Hospital -> AppUser (One-to-One)
            builder.Entity<Hospital>().HasOne(h => h.User).WithOne(u => u.Hospital).HasForeignKey<Hospital>(h => h.UserId);
            builder.Entity<Hospital>().HasIndex(h => h.LicenseNumber).IsUnique();
            builder.Entity<Hospital>().HasIndex(h => h.UserId).IsUnique();

            // Configure Doner table relationships and unique constraints
            // Donor -> AppUser (One-to-One)
            builder.Entity<Doner>().HasOne(d => d.User).WithOne(u => u.Donor).HasForeignKey<Doner>(d => d.UserId);
            builder.Entity<Doner>().HasIndex(d => d.UserId).IsUnique();
            // Donation -> Donor (Many-to-One)
            builder.Entity<Donation>().HasOne(d => d.Donor).WithMany(d => d.Donations).HasForeignKey(d => d.DonorId);



            // Configure Donation table relationships and unique constraints
            // Recipient -> Hospital (Many-to-One)

            builder.Entity<Recipient>().HasOne(r => r.Hospital).WithMany(h => h.Recipients).HasForeignKey(r => r.HospitalId);
            // Donation -> Recipient (One-to-One, optional)
            builder.Entity<Donation>().HasOne(d => d.Recipient).WithOne().HasForeignKey<Donation>(d => d.RecipientId).IsRequired(false);



        }




    }
    }
