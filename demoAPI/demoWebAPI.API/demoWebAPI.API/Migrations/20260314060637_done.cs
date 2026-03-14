using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demoWebAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productFiles_products_Productsid",
                table: "productFiles");

            migrationBuilder.DropIndex(
                name: "IX_productFiles_Productsid",
                table: "productFiles");

            migrationBuilder.DropColumn(
                name: "Productsid",
                table: "productFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Productsid",
                table: "productFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_productFiles_Productsid",
                table: "productFiles",
                column: "Productsid");

            migrationBuilder.AddForeignKey(
                name: "FK_productFiles_products_Productsid",
                table: "productFiles",
                column: "Productsid",
                principalTable: "products",
                principalColumn: "id");
        }
    }
}
