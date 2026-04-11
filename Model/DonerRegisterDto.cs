using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.Model
{
    public class DonerRegister
    {
        [Key]
        public int DonerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
