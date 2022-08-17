using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelMCShowdownAPI.Migrations
{
    public partial class Player_UUID_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_UUID",
                table: "Players",
                column: "UUID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_UUID",
                table: "Players");
        }
    }
}
