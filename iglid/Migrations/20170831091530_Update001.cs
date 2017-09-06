using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iglid.Migrations
{
    public partial class Update001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_teams_TeamID",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_teams_LeaderId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "Tname",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "teamid",
                table: "Massage",
                newName: "teamID");

            migrationBuilder.RenameColumn(
                name: "TeamID",
                table: "ApplicationUser",
                newName: "teamID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_TeamID",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_teamID");

            migrationBuilder.AlterColumn<long>(
                name: "teamID",
                table: "Massage",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_teams_LeaderId",
                table: "teams",
                column: "LeaderId",
                unique: true);


            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_teams_teamID",
                table: "ApplicationUser",
                column: "teamID",
                principalTable: "teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_teams_teamID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Massage_teams_teamID",
                table: "Massage");

            migrationBuilder.DropIndex(
                name: "IX_teams_LeaderId",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_Massage_teamID",
                table: "Massage");

            migrationBuilder.RenameColumn(
                name: "teamID",
                table: "Massage",
                newName: "teamid");

            migrationBuilder.RenameColumn(
                name: "teamID",
                table: "ApplicationUser",
                newName: "TeamID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_teamID",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_TeamID");

            migrationBuilder.AlterColumn<long>(
                name: "teamid",
                table: "Massage",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tname",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teams_LeaderId",
                table: "teams",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_teams_TeamID",
                table: "ApplicationUser",
                column: "TeamID",
                principalTable: "teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
