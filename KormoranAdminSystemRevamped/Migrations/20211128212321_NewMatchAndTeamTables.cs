using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
    public partial class NewMatchAndTeamTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tournaments",
                newName: "tournament_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "states",
                newName: "state_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "disciplines",
                newName: "discipline_id");

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    team_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TournamentId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.team_id);
                    table.ForeignKey(
                        name: "FK_teams_tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "tournaments",
                        principalColumn: "tournament_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_teams_TournamentId",
                table: "teams",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "tournament_id",
                table: "tournaments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "state_id",
                table: "states",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "discipline_id",
                table: "disciplines",
                newName: "id");
        }
    }
}
