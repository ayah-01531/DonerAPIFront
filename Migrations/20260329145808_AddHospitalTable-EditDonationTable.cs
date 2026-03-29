using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope_for_Organ_Donation.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalTableEditDonationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Donations",
                newName: "DonationName");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_HospitalId",
                table: "Donations",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Hospitals_HospitalId",
                table: "Donations",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "LicenseNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Hospitals_HospitalId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_HospitalId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "DonationName",
                table: "Donations",
                newName: "Name");
        }
    }
}
