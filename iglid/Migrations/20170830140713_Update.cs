using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iglid.Migrations
{
    public partial class Update : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "MatchId",
                table: "teams",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "teamID",
                table: "Massage",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TeamID",
                table: "ApplicationUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    bestof = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    maps = table.Column<int>(nullable: false),
                    modes = table.Column<int>(nullable: false),
                    outcome = table.Column<int>(nullable: false),
                    t1ID = table.Column<long>(nullable: true),
                    t2ID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matches_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_teams_t1ID",
                        column: x => x.t1ID,
                        principalTable: "teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_teams_t2ID",
                        column: x => x.t2ID,
                        principalTable: "teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teams_LeaderId",
                table: "teams",
                column: "LeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teams_MatchId",
                table: "teams",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Massage_teamID",
                table: "Massage",
                column: "teamID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_TeamID",
                table: "ApplicationUser",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_matches_ApplicationUserId",
                table: "matches",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_t1ID",
                table: "matches",
                column: "t1ID");

            migrationBuilder.CreateIndex(
                name: "IX_matches_t2ID",
                table: "matches",
                column: "t2ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_teams_TeamID",
                table: "ApplicationUser",
                column: "TeamID",
                principalTable: "teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_teams_teamID",
                table: "ApplicationUser",
                column: "teamID",
                principalTable: "teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Massage_teams_teamID",
                table: "Massage",
                column: "teamID",
                principalTable: "teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_matches_MatchId",
                table: "teams",
                column: "MatchId",
                principalTable: "matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_teams_TeamID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_teams_teamID",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Massage_teams_teamID",
                table: "Massage");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_matches_MatchId",
                table: "teams");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropIndex(
                name: "IX_teams_LeaderId",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_teams_MatchId",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_Massage_teamID",
                table: "Massage");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_TeamID",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "ApplicationUser");

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
