using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hope_for_Organ_Donation.Model
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }
        public int HospitalId { get; set; }
        [Required]
        public Hospital Hospital { get; set; }= null!;
        [Required]
        [MaxLength(50)]
        public string  PatientName { get; set; }= null!;
        public int PatientAge { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrganType { get; set; }=null!;
        [MaxLength(10)]
        public string BloodType { get; set; }= null!;
        public DateTime NeededBeforeDate { get; set; }   
        [Required]
        public string PhoneNumber { get; set; }= null!;
        [Required]
        public string Address { get; set; }= null!;
        [Required]
        public string NationalIDNumber { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        [Required]
        [MaxLength(20)]
        // Open / Matched / Closed
        public string Status { get; set; } = "Open";  


    }
}
