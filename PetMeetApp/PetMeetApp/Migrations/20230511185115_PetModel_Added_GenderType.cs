using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMeetApp.Migrations
{
    /// <inheritdoc />
    public partial class PetModel_Added_GenderType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GenderType",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderType",
                table: "Pets");
        }
    }
}
