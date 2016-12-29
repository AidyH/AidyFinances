using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace repository.Migrations
{
    public partial class AddUserIdtoSubcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subcategories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_UserId",
                table: "Subcategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_AspNetUsers_UserId",
                table: "Subcategories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_AspNetUsers_UserId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_UserId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subcategories");
        }
    }
}
