using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace KormoranAdminSystemRevamped.Migrations
{
	public partial class Addedusertable : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new
				{
					id = table.Column<int>(type: "int(11)", nullable: false)
						.Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
					user = table.Column<string>(type: "text", nullable: false),
					pass = table.Column<string>(type: "text", nullable: false),
					fullname = table.Column<string>(type: "text", nullable: false),
					permissions = table.Column<string>(type: "json", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_users", x => x.id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "users");
		}
	}
}
