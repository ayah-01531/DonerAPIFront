using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hope_for_Organ_Donation.Model
{
    public class Doner
    {
        [Key]
        public int DonerId { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required] 
        public string Email { get; set; } = null!;
        //Profile Donor
        [Required] 
        public string BirthDate { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string BloodType { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [StringLength(10)]
        public string NationalIDNumber { get; set; } = null!;
        public string? SelectedOrgans { get; set; }  //"Heart,Kidney,Liver"
        public bool HasSignedConsent { get; set; } = false;
        public bool IsDonationActive { get; set; } = false;
        [Required]
        public string? MedicalReportUrl { get; set; }



        public ICollection<Donation> Donations { get; set; } = new List<Donation>();

    }
}
