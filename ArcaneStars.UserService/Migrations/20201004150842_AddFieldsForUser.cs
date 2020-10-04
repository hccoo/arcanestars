using Microsoft.EntityFrameworkCore.Migrations;

namespace ArcaneStars.UserService.Migrations
{
    public partial class AddFieldsForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "channel",
                table: "users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspend",
                table: "users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "partition",
                table: "users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "channel",
                table: "users");

            migrationBuilder.DropColumn(
                name: "IsSuspend",
                table: "users");

            migrationBuilder.DropColumn(
                name: "partition",
                table: "users");
        }
    }
}
