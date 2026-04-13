using System.ComponentModel.DataAnnotations;

namespace Hope_for_Organ_Donation.DTOs
{

    public class CreateDonationDto
    {
        [Required] public string OrganType { get; set; } = null!;
    }
}
