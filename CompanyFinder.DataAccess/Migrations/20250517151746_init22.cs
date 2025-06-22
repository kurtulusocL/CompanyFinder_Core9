using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Id",
                table: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                newName: "IX_Report_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReportCategoryId",
                table: "Reports",
                newName: "IX_Report_ReportCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ProductId",
                table: "Reports",
                newName: "IX_Report_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CompanyId",
                table: "Reports",
                newName: "IX_Report_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CommentId",
                table: "Reports",
                newName: "IX_Report_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CommentAnswerId",
                table: "Reports",
                newName: "IX_Report_CommentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_BlogId",
                table: "Reports",
                newName: "IX_Report_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_ProductId",
                table: "Pictures",
                newName: "IX_Picture_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_CompanyId",
                table: "Pictures",
                newName: "IX_Picture_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_BlogId",
                table: "Pictures",
                newName: "IX_Picture_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                newName: "IX_Comment_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CompanyId",
                table: "Comments",
                newName: "IX_Comment_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                newName: "IX_Comment_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswers_UserId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswers_CommentId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswer_CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_Id",
                table: "SaveContents",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_Id",
                table: "Reports",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id",
                table: "Products",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Picture_Id",
                table: "Pictures",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_Id",
                table: "Likes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hit_Id",
                table: "Hits",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id",
                table: "Companies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Id",
                table: "Comments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_Id",
                table: "CommentAnswers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Id",
                table: "Blogs",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "AspNetUsers",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SavedContent_Id",
                table: "SaveContents");

            migrationBuilder.DropIndex(
                name: "IX_Report_Id",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Product_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Picture_Id",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Like_Id",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Hit_Id",
                table: "Hits");

            migrationBuilder.DropIndex(
                name: "IX_Company_Id",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Id",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_CommentAnswer_Id",
                table: "CommentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Blog_Id",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_User_Id",
                table: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Report_UserId",
                table: "Reports",
                newName: "IX_Reports_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_ReportCategoryId",
                table: "Reports",
                newName: "IX_Reports_ReportCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_ProductId",
                table: "Reports",
                newName: "IX_Reports_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_CompanyId",
                table: "Reports",
                newName: "IX_Reports_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_CommentId",
                table: "Reports",
                newName: "IX_Reports_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_CommentAnswerId",
                table: "Reports",
                newName: "IX_Reports_CommentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_BlogId",
                table: "Reports",
                newName: "IX_Reports_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_ProductId",
                table: "Pictures",
                newName: "IX_Pictures_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_CompanyId",
                table: "Pictures",
                newName: "IX_Pictures_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Picture_BlogId",
                table: "Pictures",
                newName: "IX_Pictures_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ProductId",
                table: "Comments",
                newName: "IX_Comments_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CompanyId",
                table: "Comments",
                newName: "IX_Comments_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_BlogId",
                table: "Comments",
                newName: "IX_Comments_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswer_UserId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentAnswer_CommentId",
                table: "CommentAnswers",
                newName: "IX_CommentAnswers_CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "AspNetUsers",
                column: "Id");
        }
    }
}
