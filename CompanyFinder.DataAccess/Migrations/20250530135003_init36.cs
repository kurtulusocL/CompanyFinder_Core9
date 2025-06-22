using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyPartnershipId",
                table: "SaveContents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyPartnershipId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyPartnershipId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyPartnershipId",
                table: "Hits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyPartnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPartnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPartnerships_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPartnerships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyPartnerships_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_CompanyPartnershipId",
                table: "SaveContents",
                column: "CompanyPartnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CompanyPartnershipId",
                table: "Reports",
                column: "CompanyPartnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CompanyPartnershipId",
                table: "Likes",
                column: "CompanyPartnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CompanyPartnershipId",
                table: "Hits",
                column: "CompanyPartnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPartnership_CompanyId",
                table: "CompanyPartnerships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPartnership_Id",
                table: "CompanyPartnerships",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPartnership_IsActive_IsDeleted",
                table: "CompanyPartnerships",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPartnership_ProductCategoryId",
                table: "CompanyPartnerships",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPartnership_UserId",
                table: "CompanyPartnerships",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_CompanyPartnerships_CompanyPartnershipId",
                table: "Hits",
                column: "CompanyPartnershipId",
                principalTable: "CompanyPartnerships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CompanyPartnerships_CompanyPartnershipId",
                table: "Likes",
                column: "CompanyPartnershipId",
                principalTable: "CompanyPartnerships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_CompanyPartnerships_CompanyPartnershipId",
                table: "Reports",
                column: "CompanyPartnershipId",
                principalTable: "CompanyPartnerships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaveContents_CompanyPartnerships_CompanyPartnershipId",
                table: "SaveContents",
                column: "CompanyPartnershipId",
                principalTable: "CompanyPartnerships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hits_CompanyPartnerships_CompanyPartnershipId",
                table: "Hits");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CompanyPartnerships_CompanyPartnershipId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_CompanyPartnerships_CompanyPartnershipId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_SaveContents_CompanyPartnerships_CompanyPartnershipId",
                table: "SaveContents");

            migrationBuilder.DropTable(
                name: "CompanyPartnerships");

            migrationBuilder.DropIndex(
                name: "IX_SavedContent_CompanyPartnershipId",
                table: "SaveContents");

            migrationBuilder.DropIndex(
                name: "IX_Report_CompanyPartnershipId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Like_CompanyPartnershipId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Hit_CompanyPartnershipId",
                table: "Hits");

            migrationBuilder.DropColumn(
                name: "CompanyPartnershipId",
                table: "SaveContents");

            migrationBuilder.DropColumn(
                name: "CompanyPartnershipId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CompanyPartnershipId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CompanyPartnershipId",
                table: "Hits");
        }
    }
}
