using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iglid.Data.Migrations
{
    public partial class Update002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "teamid",
                table: "Massage",
                newName: "teamID");

            migrationBuilder.AlterColumn<long>(
                name: "teamID",
                table: "Massage",
                nullable: true,
                oldClrType: typeof(long));


            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamID",
                table: "AspNetUsers",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Team_TeamID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Massage_Team_teamID",
                table: "Massage");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Massage_teamID",
                table: "Massage");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "teamID",
                table: "Massage",
                newName: "teamid");

            migrationBuilder.AlterColumn<long>(
                name: "teamid",
                table: "Massage",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tname",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
