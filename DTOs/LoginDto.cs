using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.DTOs
{

    public class LoginDto
    {
        [Required, EmailAddress] 
        public string Email { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
    }
}
