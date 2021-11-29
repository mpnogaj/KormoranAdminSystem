using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
    public partial class UpdatedForeignKeysNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_states_StateId",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_Team1Id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_Team2Id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_WinnerId",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_tournaments_TournamentId",
                table: "teams");

            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_disciplines_DisciplineId",
                table: "tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_states_StateId",
                table: "tournaments");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "tournaments",
                newName: "state_id");

            migrationBuilder.RenameColumn(
                name: "DisciplineId",
                table: "tournaments",
                newName: "discipline_id");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_StateId",
                table: "tournaments",
                newName: "IX_tournaments_state_id");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_DisciplineId",
                table: "tournaments",
                newName: "IX_tournaments_discipline_id");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "teams",
                newName: "tournament_id");

            migrationBuilder.RenameIndex(
                name: "IX_teams_TournamentId",
                table: "teams",
                newName: "IX_teams_tournament_id");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "matches",
                newName: "winner_id");

            migrationBuilder.RenameColumn(
                name: "Team2Id",
                table: "matches",
                newName: "team_2_id");

            migrationBuilder.RenameColumn(
                name: "Team1Id",
                table: "matches",
                newName: "team_1_id");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "matches",
                newName: "state_id");

            migrationBuilder.RenameIndex(
                name: "IX_matches_WinnerId",
                table: "matches",
                newName: "IX_matches_winner_id");

            migrationBuilder.RenameIndex(
                name: "IX_matches_Team2Id",
                table: "matches",
                newName: "IX_matches_team_2_id");

            migrationBuilder.RenameIndex(
                name: "IX_matches_Team1Id",
                table: "matches",
                newName: "IX_matches_team_1_id");

            migrationBuilder.RenameIndex(
                name: "IX_matches_StateId",
                table: "matches",
                newName: "IX_matches_state_id");

            migrationBuilder.AlterColumn<int>(
                name: "state_id",
                table: "tournaments",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "discipline_id",
                table: "tournaments",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "winner_id",
                table: "matches",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "team_2_id",
                table: "matches",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "team_1_id",
                table: "matches",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "state_id",
                table: "matches",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_states_state_id",
                table: "matches",
                column: "state_id",
                principalTable: "states",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_team_1_id",
                table: "matches",
                column: "team_1_id",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_team_2_id",
                table: "matches",
                column: "team_2_id",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_winner_id",
                table: "matches",
                column: "winner_id",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_tournaments_tournament_id",
                table: "teams",
                column: "tournament_id",
                principalTable: "tournaments",
                principalColumn: "tournament_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_disciplines_discipline_id",
                table: "tournaments",
                column: "discipline_id",
                principalTable: "disciplines",
                principalColumn: "discipline_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_states_state_id",
                table: "tournaments",
                column: "state_id",
                principalTable: "states",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_states_state_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_team_1_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_team_2_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_winner_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_tournaments_tournament_id",
                table: "teams");

            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_disciplines_discipline_id",
                table: "tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_states_state_id",
                table: "tournaments");

            migrationBuilder.RenameColumn(
                name: "state_id",
                table: "tournaments",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "discipline_id",
                table: "tournaments",
                newName: "DisciplineId");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_state_id",
                table: "tournaments",
                newName: "IX_tournaments_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_discipline_id",
                table: "tournaments",
                newName: "IX_tournaments_DisciplineId");

            migrationBuilder.RenameColumn(
                name: "tournament_id",
                table: "teams",
                newName: "TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_teams_tournament_id",
                table: "teams",
                newName: "IX_teams_TournamentId");

            migrationBuilder.RenameColumn(
                name: "winner_id",
                table: "matches",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "team_2_id",
                table: "matches",
                newName: "Team2Id");

            migrationBuilder.RenameColumn(
                name: "team_1_id",
                table: "matches",
                newName: "Team1Id");

            migrationBuilder.RenameColumn(
                name: "state_id",
                table: "matches",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_matches_winner_id",
                table: "matches",
                newName: "IX_matches_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_matches_team_2_id",
                table: "matches",
                newName: "IX_matches_Team2Id");

            migrationBuilder.RenameIndex(
                name: "IX_matches_team_1_id",
                table: "matches",
                newName: "IX_matches_Team1Id");

            migrationBuilder.RenameIndex(
                name: "IX_matches_state_id",
                table: "matches",
                newName: "IX_matches_StateId");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "tournaments",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "DisciplineId",
                table: "tournaments",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "matches",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "Team2Id",
                table: "matches",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "Team1Id",
                table: "matches",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "matches",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_states_StateId",
                table: "matches",
                column: "StateId",
                principalTable: "states",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_Team1Id",
                table: "matches",
                column: "Team1Id",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_Team2Id",
                table: "matches",
                column: "Team2Id",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_WinnerId",
                table: "matches",
                column: "WinnerId",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_tournaments_TournamentId",
                table: "teams",
                column: "TournamentId",
                principalTable: "tournaments",
                principalColumn: "tournament_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_disciplines_DisciplineId",
                table: "tournaments",
                column: "DisciplineId",
                principalTable: "disciplines",
                principalColumn: "discipline_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_states_StateId",
                table: "tournaments",
                column: "StateId",
                principalTable: "states",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
