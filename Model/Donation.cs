using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.Model
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }

        public int DonorId { get; set; }
        public Doner Donor { get; set; } = null!;

        public int? RecipientId { get; set; }
        public Recipient? Recipient { get; set; }

        [Required] 
        public string OrganType { get; set; } = null!;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}