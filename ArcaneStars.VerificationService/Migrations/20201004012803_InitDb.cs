using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArcaneStars.VerificationService.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "verifications",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(maxLength: 80, nullable: false),
                    to = table.Column<string>(maxLength: 80, nullable: false),
                    expired_on = table.Column<DateTime>(nullable: false),
                    biz_code = table.Column<int>(nullable: false),
                    is_used = table.Column<bool>(nullable: false),
                    is_suspend = table.Column<bool>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    last_upd_on = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verifications", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "verifications");
        }
    }
}
