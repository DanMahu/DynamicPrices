using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicPrices.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelStocElectronice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stoc_electronice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProdus = table.Column<int>(type: "int", nullable: false),
                    InStoc = table.Column<int>(type: "int", nullable: false),
                    StocMinim = table.Column<int>(type: "int", nullable: false),
                    StocMaxim = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stoc_electronice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_stoc_electronice_produse_electronice_IdProdus",
                        column: x => x.IdProdus,
                        principalTable: "produse_electronice",
                        principalColumn: "IdProdus",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_stoc_electronice_IdProdus",
                table: "stoc_electronice",
                column: "IdProdus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stoc_electronice");
        }
    }
}
