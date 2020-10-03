using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArcaneStars.UserService.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(maxLength: 80, nullable: false),
                    password = table.Column<string>(maxLength: 500, nullable: true),
                    mobile = table.Column<string>(maxLength: 20, nullable: false),
                    nickname = table.Column<string>(maxLength: 80, nullable: true),
                    email = table.Column<string>(maxLength: 200, nullable: true),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<string>(maxLength: 80, nullable: false),
                    updated_by = table.Column<string>(maxLength: 80, nullable: true)
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
