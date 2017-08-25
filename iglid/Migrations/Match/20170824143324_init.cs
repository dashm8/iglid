using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iglid.Migrations.Match
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    bestof = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    maps = table.Column<int>(nullable: false),
                    modes = table.Column<int>(nullable: false),
                    outcome = table.Column<int>(nullable: false),
                    t1score = table.Column<int>(nullable: false),
                    t2score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");
        }
    }
}
