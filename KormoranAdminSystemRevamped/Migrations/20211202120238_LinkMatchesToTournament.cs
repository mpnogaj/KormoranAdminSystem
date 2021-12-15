using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
    public partial class LinkMatchesToTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tournament_id",
                table: "matches",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_matches_tournament_id",
                table: "matches",
                column: "tournament_id");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_tournaments_tournament_id",
                table: "matches",
                column: "tournament_id",
                principalTable: "tournaments",
                principalColumn: "tournament_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_tournaments_tournament_id",
                table: "matches");

            migrationBuilder.DropIndex(
                name: "IX_matches_tournament_id",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "tournament_id",
                table: "matches");
        }
    }
}
