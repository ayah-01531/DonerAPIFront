using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hope_for_Organ_Donation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Hospitals_HospitalId",
                table: "Donations");

            migrationBuilder.DropTable(
                name: "DonerRegisters");

            migrationBuilder.DropIndex(
                name: "IX_Donations_HospitalId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "OrganType",
                table: "Doners");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "NationalIDNumber",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "NeededBeforeDate",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Donations",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "PatientAge",
                table: "Donations",
                newName: "DonorId");

            migrationBuilder.RenameColumn(
                name: "DonationId",
                table: "Donations",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "NationalIDNumber",
                table: "Doners",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "OrganType",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "Donations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeededOrgan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipients_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_RecipientId",
                table: "Donations",
                column: "RecipientId",
                unique: true,
                filter: "[RecipientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_HospitalId",
                table: "Recipients",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Doners_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "Doners",
                principalColumn: "DonerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Recipients_RecipientId",
                table: "Donations",
                column: "RecipientId",
                principalTable: "Recipients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Doners_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Recipients_RecipientId",
                table: "Donations");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropIndex(
                name: "IX_Donations_DonorId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_RecipientId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "DonorId",
                table: "Donations",
                newName: "PatientAge");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Donations",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Donations",
                newName: "DonationId");

            migrationBuilder.AlterColumn<string>(
                name: "NationalIDNumber",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "OrganType",
                table: "Doners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Donations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OrganType",
                table: "Donations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Donations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NationalIDNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NeededBeforeDate",
                table: "Donations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Donations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DonerRegisters",
                columns: table => new
                {
                    DonerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonerRegisters", x => x.DonerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_HospitalId",
                table: "Donations",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Hospitals_HospitalId",
                table: "Donations",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
