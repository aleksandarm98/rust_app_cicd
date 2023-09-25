using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMeetApp.Migrations
{
    /// <inheritdoc />
    public partial class changed_lostpet_notifications_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_LostPetModelId",
                table: "Notifications");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LostPetModelId",
                table: "Notifications",
                column: "LostPetModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_LostPetModelId",
                table: "Notifications");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LostPetModelId",
                table: "Notifications",
                column: "LostPetModelId",
                unique: true,
                filter: "[LostPetModelId] IS NOT NULL");
        }
    }
}
