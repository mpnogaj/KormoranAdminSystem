using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
    public partial class RemovedWinnerAndTournamentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_winner_id",
                table: "matches");

            migrationBuilder.DropIndex(
                name: "IX_matches_winner_id",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "tournament_type",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "tournament_type_short",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "winner_id",
                table: "matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tournament_type",
                table: "tournaments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tournament_type_short",
                table: "tournaments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "winner_id",
                table: "matches",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_matches_winner_id",
                table: "matches",
                column: "winner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_winner_id",
                table: "matches",
                column: "winner_id",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
