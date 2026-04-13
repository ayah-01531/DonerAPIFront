using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.DTOs
{
    public class RegisterDonorDto
    {
        [Required, EmailAddress] public string Email { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
        [Required] public string PhoneNumber { get; set; } = null!;
        [Required] public string FullName { get; set; } = null!;
    }
}
