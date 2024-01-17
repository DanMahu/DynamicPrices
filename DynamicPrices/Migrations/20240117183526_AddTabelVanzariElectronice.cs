using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicPrices.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelVanzariElectronice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vanzari_electronice",
                columns: table => new
                {
                    IdTranzactie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataTranzactie = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdProdus = table.Column<int>(type: "int", nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false),
                    PretTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vanzari_electronice", x => x.IdTranzactie);
                    table.ForeignKey(
                        name: "FK_vanzari_electronice_clienti_IdClient",
                        column: x => x.IdClient,
                        principalTable: "clienti",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vanzari_electronice_produse_electronice_IdProdus",
                        column: x => x.IdProdus,
                        principalTable: "produse_electronice",
                        principalColumn: "IdProdus",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_vanzari_electronice_IdClient",
                table: "vanzari_electronice",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_vanzari_electronice_IdProdus",
                table: "vanzari_electronice",
                column: "IdProdus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vanzari_electronice");
        }
    }
}
