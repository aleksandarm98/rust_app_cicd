using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMeetApp.Migrations
{
    /// <inheritdoc />
    public partial class change_relationship_notification_walking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WalkingModelId",
                table: "Notifications",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_WalkingModelId",
                table: "Notifications",
                column: "WalkingModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Walking_WalkingModelId",
                table: "Notifications",
                column: "WalkingModelId",
                principalTable: "Walking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Walking_WalkingModelId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_WalkingModelId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "WalkingModelId",
                table: "Notifications");
        }
    }
}
