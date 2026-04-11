namespace Hope_for_Organ_Donation.Model
{
    public class RegisterHospitalDto
    {
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string HospitalName { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
