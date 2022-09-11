using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelMCShowdownAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BattleStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleStats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UUID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlayerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ELORating = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BattleStatsPlayers",
                columns: table => new
                {
                    BattleStatsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleStatsPlayers", x => new { x.BattleStatsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_BattleStatsPlayers_BattleStats_BattleStatsId",
                        column: x => x.BattleStatsId,
                        principalTable: "BattleStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleStatsPlayers_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BattleStatsWinners",
                columns: table => new
                {
                    WinnersId = table.Column<int>(type: "int", nullable: false),
                    WonBattleStatsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleStatsWinners", x => new { x.WinnersId, x.WonBattleStatsId });
                    table.ForeignKey(
                        name: "FK_BattleStatsWinners_BattleStats_WonBattleStatsId",
                        column: x => x.WonBattleStatsId,
                        principalTable: "BattleStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleStatsWinners_Players_WinnersId",
                        column: x => x.WinnersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BattleStatsPlayers_PlayersId",
                table: "BattleStatsPlayers",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleStatsWinners_WonBattleStatsId",
                table: "BattleStatsWinners",
                column: "WonBattleStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UUID",
                table: "Players",
                column: "UUID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleStatsPlayers");

            migrationBuilder.DropTable(
                name: "BattleStatsWinners");

            migrationBuilder.DropTable(
                name: "BattleStats");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
