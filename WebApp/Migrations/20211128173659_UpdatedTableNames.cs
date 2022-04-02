using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranAdminSystemRevamped.Migrations
{
	public partial class UpdatedTableNames : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_tournaments_discipline_DisciplineId",
				table: "tournaments");

			migrationBuilder.DropForeignKey(
				name: "FK_tournaments_States_StateId",
				table: "tournaments");

			migrationBuilder.DropPrimaryKey(
				name: "PK_States",
				table: "States");

			migrationBuilder.DropPrimaryKey(
				name: "PK_discipline",
				table: "discipline");

			migrationBuilder.RenameTable(
				name: "States",
				newName: "states");

			migrationBuilder.RenameTable(
				name: "discipline",
				newName: "disciplines");

			migrationBuilder.AlterColumn<string>(
				name: "user",
				table: "users",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "pass",
				table: "users",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "fullname",
				table: "users",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type_short",
				table: "tournaments",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type",
				table: "tournaments",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "tournaments",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "states",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "disciplines",
				type: "longtext",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AddPrimaryKey(
				name: "PK_states",
				table: "states",
				column: "id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_disciplines",
				table: "disciplines",
				column: "id");

			migrationBuilder.AddForeignKey(
				name: "FK_tournaments_disciplines_DisciplineId",
				table: "tournaments",
				column: "DisciplineId",
				principalTable: "disciplines",
				principalColumn: "id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_tournaments_states_StateId",
				table: "tournaments",
				column: "StateId",
				principalTable: "states",
				principalColumn: "id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_tournaments_disciplines_DisciplineId",
				table: "tournaments");

			migrationBuilder.DropForeignKey(
				name: "FK_tournaments_states_StateId",
				table: "tournaments");

			migrationBuilder.DropPrimaryKey(
				name: "PK_states",
				table: "states");

			migrationBuilder.DropPrimaryKey(
				name: "PK_disciplines",
				table: "disciplines");

			migrationBuilder.RenameTable(
				name: "states",
				newName: "States");

			migrationBuilder.RenameTable(
				name: "disciplines",
				newName: "discipline");

			migrationBuilder.AlterColumn<string>(
				name: "user",
				table: "users",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "pass",
				table: "users",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "fullname",
				table: "users",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type_short",
				table: "tournaments",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type",
				table: "tournaments",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "tournaments",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "States",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "discipline",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "longtext")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.AddPrimaryKey(
				name: "PK_States",
				table: "States",
				column: "id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_discipline",
				table: "discipline",
				column: "id");

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
	}
}
