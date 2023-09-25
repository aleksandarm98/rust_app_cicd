using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMeetApp.Migrations
{
    /// <inheritdoc />
    public partial class rename_table_Adoption_Ad_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionAdModel_PetTypes_PetTypeId",
                table: "AdoptionAdModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionAdModel_Users_UserId",
                table: "AdoptionAdModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdoptionAdModel",
                table: "AdoptionAdModel");

            migrationBuilder.RenameTable(
                name: "AdoptionAdModel",
                newName: "AdoptionAd");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionAdModel_UserId",
                table: "AdoptionAd",
                newName: "IX_AdoptionAd_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionAdModel_PetTypeId",
                table: "AdoptionAd",
                newName: "IX_AdoptionAd_PetTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdoptionAd",
                table: "AdoptionAd",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionAd_PetTypes_PetTypeId",
                table: "AdoptionAd",
                column: "PetTypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionAd_Users_UserId",
                table: "AdoptionAd",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionAd_PetTypes_PetTypeId",
                table: "AdoptionAd");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionAd_Users_UserId",
                table: "AdoptionAd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdoptionAd",
                table: "AdoptionAd");

            migrationBuilder.RenameTable(
                name: "AdoptionAd",
                newName: "AdoptionAdModel");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionAd_UserId",
                table: "AdoptionAdModel",
                newName: "IX_AdoptionAdModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionAd_PetTypeId",
                table: "AdoptionAdModel",
                newName: "IX_AdoptionAdModel_PetTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdoptionAdModel",
                table: "AdoptionAdModel",
                column: "Id");

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
    }
}
