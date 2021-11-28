using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace KormoranAdminSystemRevamped.Migrations
{
	public partial class AddedNewEnumTables : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "tournament_type_short",
				table: "tournaments",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type",
				table: "tournaments",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "state",
				table: "tournaments",
				type: "int(11)",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "tournaments",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "game",
				table: "tournaments",
				type: "int(11)",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(string),
				oldType: "text",
				oldNullable: true);

			migrationBuilder.CreateTable(
				name: "discipline",
				columns: table => new
				{
					id = table.Column<int>(type: "int(11)", nullable: false)
						.Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
					name = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_discipline", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "States",
				columns: table => new
				{
					id = table.Column<int>(type: "int(11)", nullable: false)
						.Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
					name = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_States", x => x.id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_tournaments_game",
				table: "tournaments",
				column: "game");

			migrationBuilder.CreateIndex(
				name: "IX_tournaments_state",
				table: "tournaments",
				column: "state");

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

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_tournaments_discipline_game",
				table: "tournaments");

			migrationBuilder.DropForeignKey(
				name: "FK_tournaments_States_state",
				table: "tournaments");

			migrationBuilder.DropTable(
				name: "discipline");

			migrationBuilder.DropTable(
				name: "States");

			migrationBuilder.DropIndex(
				name: "IX_tournaments_game",
				table: "tournaments");

			migrationBuilder.DropIndex(
				name: "IX_tournaments_state",
				table: "tournaments");

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type_short",
				table: "tournaments",
				type: "text",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "text");

			migrationBuilder.AlterColumn<string>(
				name: "tournament_type",
				table: "tournaments",
				type: "text",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "text");

			migrationBuilder.AlterColumn<string>(
				name: "state",
				table: "tournaments",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int(11)");

			migrationBuilder.AlterColumn<string>(
				name: "name",
				table: "tournaments",
				type: "text",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "text");

			migrationBuilder.AlterColumn<string>(
				name: "game",
				table: "tournaments",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int(11)");
		}
	}
}
