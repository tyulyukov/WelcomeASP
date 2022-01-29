using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeASP.Data.Migrations
{
    public partial class ChangeIdentityUserWithTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "AspNetUsers");
        }
    }
}
