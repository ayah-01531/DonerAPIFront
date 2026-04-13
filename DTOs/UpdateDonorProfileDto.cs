namespace Hope_for_Organ_Donation.DTOs
{
    public class UpdateDonorProfileDto
    {
        public string BloodType { get; set; } = null!;
        public string Address { get; set; }= null!;
        public string? SelectedOrgans { get; set; }
        public string BirthDate { get; set; }= null!;
        public string NationalIDNumber { get; set; } = null!;
        public string? MedicalReportUrl { get; set; }


        public bool? HasSignedConsent { get; set; }
        public bool? IsDonationActive { get; set; }
    }
}
