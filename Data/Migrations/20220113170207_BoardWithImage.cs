using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeASP.Data.Migrations
{
    public partial class BoardWithImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Boards",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Boards");
        }
    }
}
