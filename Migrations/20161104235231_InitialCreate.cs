using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace newly_read_core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Autoincrement", true),
                    author = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    jsonstring = table.Column<string>(nullable: true),
                    provider_name = table.Column<string>(nullable: true),
                    publishedAt = table.Column<string>(nullable: true),
                    timestamp = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    urlToImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    category = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    sortBysAvailable = table.Column<string>(nullable: true),
                    sourceID = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    urlsToLogos = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
