using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMeetApp.Migrations
{
    /// <inheritdoc />
    public partial class table_Walking_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WalkingModelId",
                table: "Pets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Walking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationX = table.Column<double>(type: "float", nullable: false),
                    LocationY = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_WalkingModelId",
                table: "Pets",
                column: "WalkingModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Walking_WalkingModelId",
                table: "Pets",
                column: "WalkingModelId",
                principalTable: "Walking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Walking_WalkingModelId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "Walking");

            migrationBuilder.DropIndex(
                name: "IX_Pets_WalkingModelId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "WalkingModelId",
                table: "Pets");
        }
    }
}
