using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NapoleonCRM.Migrations
{
    /// <inheritdoc />
    public partial class AddingCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "Customer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId1",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CurrencyId1",
                table: "Customer",
                column: "CurrencyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Currency_CurrencyId1",
                table: "Customer",
                column: "CurrencyId1",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Currency_CurrencyId1",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CurrencyId1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CurrencyId1",
                table: "Customer");
        }
    }
}
