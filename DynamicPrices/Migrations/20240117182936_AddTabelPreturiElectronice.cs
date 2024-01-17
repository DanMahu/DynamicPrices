using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicPrices.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelPreturiElectronice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "preturi_electronice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProdus = table.Column<int>(type: "int", nullable: false),
                    PretCurent = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataActualizare = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preturi_electronice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_preturi_electronice_produse_electronice_IdProdus",
                        column: x => x.IdProdus,
                        principalTable: "produse_electronice",
                        principalColumn: "IdProdus",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_preturi_electronice_IdProdus",
                table: "preturi_electronice",
                column: "IdProdus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "preturi_electronice");
        }
    }
}
