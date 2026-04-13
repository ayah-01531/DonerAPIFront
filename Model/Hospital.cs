using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.Model
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }
        public AppUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [Required]
        public string LicenseNumber { get; set; } = null!;
        [Required]
        public string HospitalName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        public bool IsApproved { get; set; } = false;
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();




    }
}
