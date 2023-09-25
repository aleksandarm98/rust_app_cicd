using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMeetApp.Migrations
{
    /// <inheritdoc />
    public partial class add_user_relationship_Adoption_Ad_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetType",
                table: "AdoptionAdModel");

            migrationBuilder.AddColumn<long>(
                name: "PetTypeId",
                table: "AdoptionAdModel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AdoptionAdModel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionAdModel_PetTypeId",
                table: "AdoptionAdModel",
                column: "PetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionAdModel_UserId",
                table: "AdoptionAdModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionAdModel_PetTypes_PetTypeId",
                table: "AdoptionAdModel",
                column: "PetTypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionAdModel_Users_UserId",
                table: "AdoptionAdModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionAdModel_PetTypes_PetTypeId",
                table: "AdoptionAdModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionAdModel_Users_UserId",
                table: "AdoptionAdModel");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionAdModel_PetTypeId",
                table: "AdoptionAdModel");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionAdModel_UserId",
                table: "AdoptionAdModel");

            migrationBuilder.DropColumn(
                name: "PetTypeId",
                table: "AdoptionAdModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdoptionAdModel");

            migrationBuilder.AddColumn<string>(
                name: "PetType",
                table: "AdoptionAdModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
