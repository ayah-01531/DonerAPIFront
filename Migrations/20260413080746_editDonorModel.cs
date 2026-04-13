using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope_for_Organ_Donation.Migrations
{
    /// <inheritdoc />
    public partial class editDonorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSignedConsent",
                table: "Doners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDonationActive",
                table: "Doners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MedicalReportUrl",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedOrgans",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSignedConsent",
                table: "Doners");

            migrationBuilder.DropColumn(
                name: "IsDonationActive",
                table: "Doners");

            migrationBuilder.DropColumn(
                name: "MedicalReportUrl",
                table: "Doners");

            migrationBuilder.DropColumn(
                name: "SelectedOrgans",
                table: "Doners");
        }
    }
}
