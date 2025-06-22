using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_SaveContents_UserId",
                table: "SaveContents",
                newName: "IX_SavedContent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SaveContents_ProductId",
                table: "SaveContents",
                newName: "IX_SavedContent_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SaveContents_CompanyId",
                table: "SaveContents",
                newName: "IX_SavedContent_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_SaveContents_BlogId",
                table: "SaveContents",
                newName: "IX_SavedContent_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Product_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductSubcategoryId",
                table: "Products",
                newName: "IX_Product_ProductSubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                newName: "IX_Product_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                newName: "IX_Product_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ProductId",
                table: "Likes",
                newName: "IX_Like_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_CompanyId",
                table: "Likes",
                newName: "IX_Like_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                newName: "IX_Like_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_CommentAnswerId",
                table: "Likes",
                newName: "IX_Like_CommentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_BlogId",
                table: "Likes",
                newName: "IX_Like_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_UserId",
                table: "Hits",
                newName: "IX_Hit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_ProductId",
                table: "Hits",
                newName: "IX_Hit_PoruductId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_CompanyId",
                table: "Hits",
                newName: "IX_Hit_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_CommentId",
                table: "Hits",
                newName: "IX_Hit_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_CommentAnswerId",
                table: "Hits",
                newName: "IX_Hit_CommentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_BlogId",
                table: "Hits",
                newName: "IX_Hit_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Hits_AdId",
                table: "Hits",
                newName: "IX_Hit_AdId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                newName: "IX_Company_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                newName: "IX_Company_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_CompanySubcategoryId",
                table: "Companies",
                newName: "IX_Company_CompanySubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_CompanyCategoryId",
                table: "Companies",
                newName: "IX_Company_CompanyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                newName: "IX_Company_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                newName: "IX_Blog_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_CompanyId",
                table: "Blogs",
                newName: "IX_Blog_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "Blogs",
                newName: "IX_Blog_BlogCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameSurname",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_SavedDate",
                table: "SaveContents",
                column: "SavedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Price",
                table: "Products",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Title",
                table: "Blogs",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NameSurname",
                table: "AspNetUsers",
                column: "NameSurname");

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
                name: "IX_SavedContent_SavedDate",
                table: "SaveContents");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Price",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Name",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_Title",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NameSurname",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_User_Id",
                table: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SavedContent_UserId",
                table: "SaveContents",
                newName: "IX_SaveContents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedContent_ProductId",
                table: "SaveContents",
                newName: "IX_SaveContents_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedContent_CompanyId",
                table: "SaveContents",
                newName: "IX_SaveContents_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedContent_BlogId",
                table: "SaveContents",
                newName: "IX_SaveContents_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductSubcategoryId",
                table: "Products",
                newName: "IX_Products_ProductSubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Products",
                newName: "IX_Products_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CompanyId",
                table: "Products",
                newName: "IX_Products_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ProductId",
                table: "Likes",
                newName: "IX_Likes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_CompanyId",
                table: "Likes",
                newName: "IX_Likes_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_CommentId",
                table: "Likes",
                newName: "IX_Likes_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_CommentAnswerId",
                table: "Likes",
                newName: "IX_Likes_CommentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_BlogId",
                table: "Likes",
                newName: "IX_Likes_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_UserId",
                table: "Hits",
                newName: "IX_Hits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_PoruductId",
                table: "Hits",
                newName: "IX_Hits_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_CompanyId",
                table: "Hits",
                newName: "IX_Hits_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_CommentId",
                table: "Hits",
                newName: "IX_Hits_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_CommentAnswerId",
                table: "Hits",
                newName: "IX_Hits_CommentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_BlogId",
                table: "Hits",
                newName: "IX_Hits_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_AdId",
                table: "Hits",
                newName: "IX_Hits_AdId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_UserId",
                table: "Companies",
                newName: "IX_Companies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_CountryId",
                table: "Companies",
                newName: "IX_Companies_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_CompanySubcategoryId",
                table: "Companies",
                newName: "IX_Companies_CompanySubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_CompanyCategoryId",
                table: "Companies",
                newName: "IX_Companies_CompanyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_CityId",
                table: "Companies",
                newName: "IX_Companies_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_UserId",
                table: "Blogs",
                newName: "IX_Blogs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_CompanyId",
                table: "Blogs",
                newName: "IX_Blogs_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCategoryId",
                table: "Blogs",
                newName: "IX_Blogs_BlogCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NameSurname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
