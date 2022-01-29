using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WelcomeASP.Data.Migrations
{
    public partial class Portfolio_Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioItems_PortfolioCategories_CategoryId",
                table: "PortfolioItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "PortfolioItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioItems_PortfolioCategories_CategoryId",
                table: "PortfolioItems",
                column: "CategoryId",
                principalTable: "PortfolioCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioItems_PortfolioCategories_CategoryId",
                table: "PortfolioItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "PortfolioItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioItems_PortfolioCategories_CategoryId",
                table: "PortfolioItems",
                column: "CategoryId",
                principalTable: "PortfolioCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
