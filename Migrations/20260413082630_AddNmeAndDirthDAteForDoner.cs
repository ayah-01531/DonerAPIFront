using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope_for_Organ_Donation.Migrations
{
    /// <inheritdoc />
    public partial class AddNmeAndDirthDAteForDoner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthDate",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Doners");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doners");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Doners");
        }
    }
}
