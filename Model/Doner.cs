using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.Model
{
    public class Doner
    {
        [Key]
        public int DonerId { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        [Required]
        public string BloodType { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string NationalIDNumber { get; set; } = null!;
        [Required]
        public string OrganType { get; set; } = null!;

    }
}
