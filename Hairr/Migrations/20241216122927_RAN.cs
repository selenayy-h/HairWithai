using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hairr.Migrations
{
    /// <inheritdoc />
    public partial class RAN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "UygunlukBaslangic",
                table: "Personels",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "UygunlukBitis",
                table: "Personels",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "UygunlukGunler",
                table: "Personels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UygunlukBaslangic",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "UygunlukBitis",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "UygunlukGunler",
                table: "Personels");
        }
    }
}
