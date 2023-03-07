using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIS521Assign3Lmthompson.Data.Migrations
{
    public partial class addedpictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Poster",
                table: "Movie",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Actor",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Actor");
        }
    }
}
