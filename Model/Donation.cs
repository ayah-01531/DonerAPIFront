using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hope_for_Organ_Donation.Model
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }
        [Required]
        public string DonationName { get; set; }
        [Required]
        public string BloodType { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string NationalIDNumber { get; set; }
        [Required]
        public string OrganType { get; set; }
        public DateTime RegistrationDate { get; set; }
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
