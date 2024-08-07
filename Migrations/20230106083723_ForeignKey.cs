using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AtributiUArtiklu_ArtiklID",
                table: "AtributiUArtiklu",
                column: "ArtiklID");

            migrationBuilder.AddForeignKey(
                name: "FK_AtributiUArtiklu_Artikli_ArtiklID",
                table: "AtributiUArtiklu",
                column: "ArtiklID",
                principalTable: "Artikli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AtributiUArtiklu_Atributi_AtributID",
                table: "AtributiUArtiklu",
                column: "AtributID",
                principalTable: "Atributi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtributiUArtiklu_Artikli_ArtiklID",
                table: "AtributiUArtiklu");

            migrationBuilder.DropForeignKey(
                name: "FK_AtributiUArtiklu_Atributi_AtributID",
                table: "AtributiUArtiklu");

            migrationBuilder.DropIndex(
                name: "IX_AtributiUArtiklu_ArtiklID",
                table: "AtributiUArtiklu");
        }
    }
}
