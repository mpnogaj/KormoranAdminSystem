using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace KormoranWeb.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disciplines",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplines", x => x.discipline_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    level = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    author = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    state_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.state_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pass = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fullname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permissions = table.Column<string>(type: "json", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    tournament_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    discipline_id = table.Column<int>(type: "int(11)", nullable: false),
                    state_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournaments", x => x.tournament_id);
                    table.ForeignKey(
                        name: "FK_tournaments_disciplines_discipline_id",
                        column: x => x.discipline_id,
                        principalTable: "disciplines",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tournaments_states_state_id",
                        column: x => x.state_id,
                        principalTable: "states",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    team_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tournament_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.team_id);
                    table.ForeignKey(
                        name: "FK_teams_tournaments_tournament_id",
                        column: x => x.tournament_id,
                        principalTable: "tournaments",
                        principalColumn: "tournament_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    match_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tournament_id = table.Column<int>(type: "int(11)", nullable: false),
                    state_id = table.Column<int>(type: "int(11)", nullable: false),
                    team_1_id = table.Column<int>(type: "int(11)", nullable: false),
                    team_2_id = table.Column<int>(type: "int(11)", nullable: false),
                    team_1_score = table.Column<int>(type: "int(11)", nullable: false),
                    team_2_score = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.match_id);
                    table.ForeignKey(
                        name: "FK_matches_states_state_id",
                        column: x => x.state_id,
                        principalTable: "states",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_teams_team_1_id",
                        column: x => x.team_1_id,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_teams_team_2_id",
                        column: x => x.team_2_id,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_matches_tournaments_tournament_id",
                        column: x => x.tournament_id,
                        principalTable: "tournaments",
                        principalColumn: "tournament_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_matches_state_id",
                table: "matches",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_team_1_id",
                table: "matches",
                column: "team_1_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_team_2_id",
                table: "matches",
                column: "team_2_id");

            migrationBuilder.CreateIndex(
                name: "IX_matches_tournament_id",
                table: "matches",
                column: "tournament_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_tournament_id",
                table: "teams",
                column: "tournament_id");

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_discipline_id",
                table: "tournaments",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_state_id",
                table: "tournaments",
                column: "state_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "disciplines");

            migrationBuilder.DropTable(
                name: "states");
        }
    }
}