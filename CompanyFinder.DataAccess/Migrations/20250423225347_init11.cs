using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogSubcategories_BlogSubcategoryId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogSubcategories");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogSubcategoryId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogSubcategoryId",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogSubcategoryId",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogSubcategories_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogSubcategoryId",
                table: "Blogs",
                column: "BlogSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogSubcategories_BlogCategoryId",
                table: "BlogSubcategories",
                column: "BlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogSubcategories_BlogSubcategoryId",
                table: "Blogs",
                column: "BlogSubcategoryId",
                principalTable: "BlogSubcategories",
                principalColumn: "Id");
        }
    }
}
