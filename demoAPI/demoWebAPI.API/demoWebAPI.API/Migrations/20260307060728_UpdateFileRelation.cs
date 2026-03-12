using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demoWebAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFileRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "fileModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_fileModels_StateId",
                table: "fileModels",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_fileModels_States_StateId",
                table: "fileModels",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fileModels_States_StateId",
                table: "fileModels");

            migrationBuilder.DropIndex(
                name: "IX_fileModels_StateId",
                table: "fileModels");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "fileModels");
        }
    }
}
