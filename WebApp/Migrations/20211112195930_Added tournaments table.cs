using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace KormoranAdminSystemRevamped.Migrations
{
	public partial class Addedtournamentstable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "tournaments",
				columns: table => new
				{
					id = table.Column<int>(type: "int(11)", nullable: false)
						.Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
					name = table.Column<string>(type: "text", nullable: true),
					game = table.Column<string>(type: "text", nullable: true),
					state = table.Column<string>(type: "text", nullable: true),
					tournament_type = table.Column<string>(type: "text", nullable: true),
					tournament_type_short = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_tournaments", x => x.id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tournaments");
		}
	}
}
