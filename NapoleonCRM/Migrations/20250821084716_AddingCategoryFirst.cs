using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NapoleonCRM.Migrations
{
    /// <inheritdoc />
    public partial class AddingCategoryFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryFirstId",
                table: "Customer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryFirstId1",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryFirst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFirst", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CategoryFirstId1",
                table: "Customer",
                column: "CategoryFirstId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CategoryFirst_CategoryFirstId1",
                table: "Customer",
                column: "CategoryFirstId1",
                principalTable: "CategoryFirst",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CategoryFirst_CategoryFirstId1",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "CategoryFirst");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CategoryFirstId1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CategoryFirstId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CategoryFirstId1",
                table: "Customer");
        }
    }
}
