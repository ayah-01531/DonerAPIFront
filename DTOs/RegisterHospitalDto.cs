using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.DTOs
{
    public class RegisterHospitalDto
    {
        [Required, EmailAddress] 
        public string Email { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required] 
        public string HospitalName { get; set; } = null!;
        [Required]
        public string LicenseNumber { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required] 
        public string City { get; set; } = null!;
    }
}
