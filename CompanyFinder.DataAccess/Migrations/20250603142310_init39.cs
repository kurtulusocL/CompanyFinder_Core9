using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts",
                newName: "IX_CompanyContact_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyFormAnswerId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyFormId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyFormId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyFormId",
                table: "Hits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHide",
                table: "CompanyContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FormCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAnswerable = table.Column<bool>(type: "bit", nullable: false),
                    FormCategoryId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyForms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyForms_FormCategories_FormCategoryId",
                        column: x => x.FormCategoryId,
                        principalTable: "FormCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyFormAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyFormId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_CompanyFormAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFormAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyFormAnswers_CompanyForms_CompanyFormId",
                        column: x => x.CompanyFormId,
                        principalTable: "CompanyForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CompanyFormAnswerId",
                table: "Reports",
                column: "CompanyFormAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CompanyFormId",
                table: "Reports",
                column: "CompanyFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CompanyFormId",
                table: "Likes",
                column: "CompanyFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Hits_CompanyFormId",
                table: "Hits",
                column: "CompanyFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_Id",
                table: "CompanyContacts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_IsActive_IsDeleted",
                table: "CompanyContacts",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_IsHide",
                table: "CompanyContacts",
                column: "IsHide");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFormAnswer_CompanyFormId",
                table: "CompanyFormAnswers",
                column: "CompanyFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFormAnswer_Id",
                table: "CompanyFormAnswers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFormAnswer_IsActive_IsDeleted",
                table: "CompanyFormAnswers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFormAnswer_UserId",
                table: "CompanyFormAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyForm_CompanyId",
                table: "CompanyForms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyForm_FormCategoryId",
                table: "CompanyForms",
                column: "FormCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyForm_Id",
                table: "CompanyForms",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyForm_IsActive_IsDeleted",
                table: "CompanyForms",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_FormCategory_Id",
                table: "FormCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormCategory_IsActive_IsDeleted",
                table: "FormCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_CompanyForms_CompanyFormId",
                table: "Hits",
                column: "CompanyFormId",
                principalTable: "CompanyForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CompanyForms_CompanyFormId",
                table: "Likes",
                column: "CompanyFormId",
                principalTable: "CompanyForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_CompanyFormAnswers_CompanyFormAnswerId",
                table: "Reports",
                column: "CompanyFormAnswerId",
                principalTable: "CompanyFormAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_CompanyForms_CompanyFormId",
                table: "Reports",
                column: "CompanyFormId",
                principalTable: "CompanyForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hits_CompanyForms_CompanyFormId",
                table: "Hits");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CompanyForms_CompanyFormId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_CompanyFormAnswers_CompanyFormAnswerId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_CompanyForms_CompanyFormId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "CompanyFormAnswers");

            migrationBuilder.DropTable(
                name: "CompanyForms");

            migrationBuilder.DropTable(
                name: "FormCategories");

            migrationBuilder.DropIndex(
                name: "IX_Reports_CompanyFormAnswerId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_CompanyFormId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CompanyFormId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Hits_CompanyFormId",
                table: "Hits");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_Id",
                table: "CompanyContacts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_IsActive_IsDeleted",
                table: "CompanyContacts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_IsHide",
                table: "CompanyContacts");

            migrationBuilder.DropColumn(
                name: "CompanyFormAnswerId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CompanyFormId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "CompanyFormId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CompanyFormId",
                table: "Hits");

            migrationBuilder.DropColumn(
                name: "IsHide",
                table: "CompanyContacts");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyContact_CompanyId",
                table: "CompanyContacts",
                newName: "IX_CompanyContacts_CompanyId");
        }
    }
}
