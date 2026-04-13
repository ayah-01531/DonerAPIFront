using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.Model
{
    public class Recipient
    {
        [Key]
        public int Id { get; set; }

        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; } = null!;

        [Required] 
        public string FullName { get; set; } = null!;
        [Required]
        public string BloodType { get; set; } = null!;
        [Required]
        public int Age { get; set; }

        [Required] 
        public string NeededOrgan { get; set; } = null!;
        public string Status { get; set; } = "Waiting";
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
