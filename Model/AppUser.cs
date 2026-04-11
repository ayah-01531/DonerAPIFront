using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Hope_for_Organ_Donation.Model
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Hospital? Hospital { get; set; }
        public Doner? Donor { get; set; }

    }
}
