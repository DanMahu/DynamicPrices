using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicPrices.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelIstoricPreturiElectronice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "istoric_preturi_electronice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProdus = table.Column<int>(type: "int", nullable: false),
                    PretVechi = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PretNou = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataModificare = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_istoric_preturi_electronice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_istoric_preturi_electronice_produse_electronice_IdProdus",
                        column: x => x.IdProdus,
                        principalTable: "produse_electronice",
                        principalColumn: "IdProdus",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_istoric_preturi_electronice_IdProdus",
                table: "istoric_preturi_electronice",
                column: "IdProdus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "istoric_preturi_electronice");
        }
    }
}
