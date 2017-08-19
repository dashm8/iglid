using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iglid.Migrations
{
    public partial class teamid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaderId",
                table: "teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "senderId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PSN = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TeamID = table.Column<long>(nullable: true),
                    Tname = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teams_LeaderId",
                table: "teams",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_senderId",
                table: "Requests",
                column: "senderId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_TeamID",
                table: "ApplicationUser",
                column: "TeamID");


            migrationBuilder.AddForeignKey(
                name: "FK_Requests_ApplicationUser_senderId",
                table: "Requests",
                column: "senderId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_ApplicationUser_LeaderId",
                table: "teams",
                column: "LeaderId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_ApplicationUser_senderId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_ApplicationUser_LeaderId",
                table: "teams");

            migrationBuilder.DropTable(
                name: "Massage");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_teams_LeaderId",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_Requests_senderId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "senderId",
                table: "Requests");
        }
    }
}
