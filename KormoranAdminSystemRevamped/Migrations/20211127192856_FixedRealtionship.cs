using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
    public partial class FixedRealtionship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_discipline_game",
                table: "tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_States_state",
                table: "tournaments");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "tournaments",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "game",
                table: "tournaments",
                newName: "DisciplineId");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_state",
                table: "tournaments",
                newName: "IX_tournaments_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_game",
                table: "tournaments",
                newName: "IX_tournaments_DisciplineId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_discipline_DisciplineId",
                table: "tournaments",
                column: "DisciplineId",
                principalTable: "discipline",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_States_StateId",
                table: "tournaments",
                column: "StateId",
                principalTable: "States",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_discipline_DisciplineId",
                table: "tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_States_StateId",
                table: "tournaments");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "tournaments",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "DisciplineId",
                table: "tournaments",
                newName: "game");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_StateId",
                table: "tournaments",
                newName: "IX_tournaments_state");

            migrationBuilder.RenameIndex(
                name: "IX_tournaments_DisciplineId",
                table: "tournaments",
                newName: "IX_tournaments_game");

            migrationBuilder.AlterColumn<int>(
                name: "state",
                table: "tournaments",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "game",
                table: "tournaments",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_discipline_game",
                table: "tournaments",
                column: "game",
                principalTable: "discipline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_States_state",
                table: "tournaments",
                column: "state",
                principalTable: "States",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
