using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
    public partial class FixedProblemsWithNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    match_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateId = table.Column<int>(type: "int(11)", nullable: true),
                    Team1Id = table.Column<int>(type: "int(11)", nullable: true),
                    Team2Id = table.Column<int>(type: "int(11)", nullable: true),
                    WinnerId = table.Column<int>(type: "int(11)", nullable: true),
                    team_1_score = table.Column<int>(type: "int(11)", nullable: false),
                    team_2_score = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.match_id);
                    table.ForeignKey(
                        name: "FK_matches_states_StateId",
                        column: x => x.StateId,
                        principalTable: "states",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_teams_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_teams_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_teams_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_matches_StateId",
                table: "matches",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_Team1Id",
                table: "matches",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_Team2Id",
                table: "matches",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_WinnerId",
                table: "matches",
                column: "WinnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");
        }
    }
}
