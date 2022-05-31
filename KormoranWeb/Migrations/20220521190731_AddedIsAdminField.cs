using Microsoft.EntityFrameworkCore.Migrations;

namespace KormoranWeb.Migrations
{
	public partial class AddedIsAdminField : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "permissions",
				table: "users");

			migrationBuilder.AddColumn<bool>(
				name: "is_admin",
				table: "users",
				type: "tinyint(1)",
				nullable: false,
				defaultValue: false);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "is_admin",
				table: "users");

			migrationBuilder.AddColumn<string>(
				name: "permissions",
				table: "users",
				type: "json",
				nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");
		}
	}
}
