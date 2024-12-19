using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hairr.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonelUygunlukGun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UygunlukGunler",
                table: "Personels");

            migrationBuilder.CreateTable(
                name: "PersonelUygunlukGunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gun = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelUygunlukGunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelUygunlukGunler_Personels_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personels",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelUygunlukGunler_PersonelId",
                table: "PersonelUygunlukGunler",
                column: "PersonelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelUygunlukGunler");

            migrationBuilder.AddColumn<string>(
                name: "UygunlukGunler",
                table: "Personels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
