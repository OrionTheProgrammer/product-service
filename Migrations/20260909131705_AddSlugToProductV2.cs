using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Service.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToProductV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSlug",
                table: "Products",
                column: "ProductSlug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductSlug",
                table: "Products");
        }
    }
}
