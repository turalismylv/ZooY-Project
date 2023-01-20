using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ProductTagsModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesTags_Products_ProductId",
                table: "CategoriesTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesTags_Tags_TagId",
                table: "CategoriesTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesTags",
                table: "CategoriesTags");

            migrationBuilder.RenameTable(
                name: "CategoriesTags",
                newName: "ProductTags");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriesTags_TagId",
                table: "ProductTags",
                newName: "IX_ProductTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriesTags_ProductId",
                table: "ProductTags",
                newName: "IX_ProductTags_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Products_ProductId",
                table: "ProductTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Products_ProductId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "CategoriesTags");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_TagId",
                table: "CategoriesTags",
                newName: "IX_CategoriesTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_ProductId",
                table: "CategoriesTags",
                newName: "IX_CategoriesTags_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesTags",
                table: "CategoriesTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesTags_Products_ProductId",
                table: "CategoriesTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesTags_Tags_TagId",
                table: "CategoriesTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
