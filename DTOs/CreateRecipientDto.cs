using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.DTOs
{
    public class CreateRecipientDto
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string BloodType { get; set; } = null!;
        [Required]
        public string NeededOrgan { get; set; } = null!;
        public int Age { get; set; }

    }
}
