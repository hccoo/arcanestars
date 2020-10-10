using Microsoft.EntityFrameworkCore.Migrations;

namespace ArcaneStars.JPService.Migrations
{
    public partial class AddFieldsForRecommendAndCommend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "question_id",
                table: "recommends",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "recommend_id",
                table: "recommend_specs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "recommend_id",
                table: "comments",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "question_id",
                table: "recommends");

            migrationBuilder.DropColumn(
                name: "recommend_id",
                table: "recommend_specs");

            migrationBuilder.DropColumn(
                name: "recommend_id",
                table: "comments");
        }
    }
}
