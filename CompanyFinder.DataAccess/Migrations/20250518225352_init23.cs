using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                newName: "IX_UserSession_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDos_UserId",
                table: "ToDos",
                newName: "IX_ToDo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileImages_UserId",
                table: "ProfileImages",
                newName: "IX_ProfileImage_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubcategories_ProductCategoryId",
                table: "ProductSubcategories",
                newName: "IX_ProductSubcategory_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CustomerStatusId",
                table: "Customers",
                newName: "IX_Customer_CustomerStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CompanyId",
                table: "Customers",
                newName: "IX_Customer_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySubcategories_CompanyCategoryId",
                table: "CompanySubcategories",
                newName: "IX_CompanySubcategory_CompanyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyMessages_UserId",
                table: "CompanyMessages",
                newName: "IX_CompanyMessage_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyMessages_CompanyId",
                table: "CompanyMessages",
                newName: "IX_CompanyMessage_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                newName: "IX_City_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CancelMemberships_UserId",
                table: "CancelMemberships",
                newName: "IX_CancelMembership_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CancelMemberships_CancelMembershipReasonId",
                table: "CancelMemberships",
                newName: "IX_CancelMembership_CancelMembershipReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                newName: "IX_Appointment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CompanyId",
                table: "Appointments",
                newName: "IX_Appointment_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentAnswers_AppointmentId",
                table: "AppointmentAnswers",
                newName: "IX_AppointmentAnswer_AppointmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserSessions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_Id",
                table: "UserSessions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_IsActive_IsDeleted",
                table: "UserSessions",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_LoginDate",
                table: "UserSessions",
                column: "LoginDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_Username",
                table: "UserSessions",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_Id",
                table: "ToDos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_IsActive_IsDeleted",
                table: "ToDos",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_SavedContent_IsActive_IsDeleted",
                table: "SaveContents",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Report_IsActive_IsDeleted",
                table: "Reports",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_Id",
                table: "QuestionAnswer",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_IsActive_IsDeleted",
                table: "QuestionAnswer",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_Id",
                table: "Question",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_IsActive_IsDeleted",
                table: "Question",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_Id",
                table: "ProfileImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_IsActive_IsDeleted",
                table: "ProfileImages",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategory_Id",
                table: "ProductSubcategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubcategory_IsActive_IsDeleted",
                table: "ProductSubcategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_IsActive_IsDeleted",
                table: "Products",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_Id",
                table: "ProductCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_IsActive_IsDeleted",
                table: "ProductCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_IsActive_IsDeleted",
                table: "Pictures",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Like_IsActive_IsDeleted",
                table: "Likes",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Hit_IsActive_IsDeleted",
                table: "Hits",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Id",
                table: "Customers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IsActive_IsDeleted",
                table: "Customers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Id",
                table: "Countries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_IsActive_IsDeleted",
                table: "Countries",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubcategory_Id",
                table: "CompanySubcategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySubcategory_IsActive_IsDeleted",
                table: "CompanySubcategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMessage_Id",
                table: "CompanyMessages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMessage_IsActive_IsDeleted",
                table: "CompanyMessages",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategory_Id",
                table: "CompanyCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCategory_IsActive_IsDeleted",
                table: "CompanyCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Company_IsActive_IsDeleted",
                table: "Companies",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IsActive_IsDeleted",
                table: "Comments",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAnswer_IsActive_IsDeleted",
                table: "CommentAnswers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_City_Id",
                table: "Cities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_IsActive_IsDeleted",
                table: "Cities",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembership_Id",
                table: "CancelMemberships",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CancelMembership_IsActive_IsDeleted",
                table: "CancelMemberships",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_IsActive_IsDeleted",
                table: "Blogs",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_Id",
                table: "BlogCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_IsActive_IsDeleted",
                table: "BlogCategories",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_User_IsActive_IsDeleted",
                table: "AspNetUsers",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_IsActive_IsDeleted",
                table: "Appointments",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Id",
                table: "Appointments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentAnswer_Id",
                table: "AppointmentAnswers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentAnswer_IsActive_IsDeleted",
                table: "AppointmentAnswers",
                columns: new[] { "IsActive", "IsDeleted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSession_Id",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_IsActive_IsDeleted",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_LoginDate",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_Username",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_Id",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDo_IsActive_IsDeleted",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_SavedContent_IsActive_IsDeleted",
                table: "SaveContents");

            migrationBuilder.DropIndex(
                name: "IX_Report_IsActive_IsDeleted",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnswer_Id",
                table: "QuestionAnswer");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnswer_IsActive_IsDeleted",
                table: "QuestionAnswer");

            migrationBuilder.DropIndex(
                name: "IX_Question_Id",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_IsActive_IsDeleted",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImage_Id",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImage_IsActive_IsDeleted",
                table: "ProfileImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubcategory_Id",
                table: "ProductSubcategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductSubcategory_IsActive_IsDeleted",
                table: "ProductSubcategories");

            migrationBuilder.DropIndex(
                name: "IX_Product_IsActive_IsDeleted",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_Id",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_IsActive_IsDeleted",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_Picture_IsActive_IsDeleted",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Like_IsActive_IsDeleted",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Hit_IsActive_IsDeleted",
                table: "Hits");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Id",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customer_IsActive_IsDeleted",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Country_Id",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Country_IsActive_IsDeleted",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_CompanySubcategory_Id",
                table: "CompanySubcategories");

            migrationBuilder.DropIndex(
                name: "IX_CompanySubcategory_IsActive_IsDeleted",
                table: "CompanySubcategories");

            migrationBuilder.DropIndex(
                name: "IX_CompanyMessage_Id",
                table: "CompanyMessages");

            migrationBuilder.DropIndex(
                name: "IX_CompanyMessage_IsActive_IsDeleted",
                table: "CompanyMessages");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCategory_Id",
                table: "CompanyCategories");

            migrationBuilder.DropIndex(
                name: "IX_CompanyCategory_IsActive_IsDeleted",
                table: "CompanyCategories");

            migrationBuilder.DropIndex(
                name: "IX_Company_IsActive_IsDeleted",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Comment_IsActive_IsDeleted",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_CommentAnswer_IsActive_IsDeleted",
                table: "CommentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_City_Id",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_City_IsActive_IsDeleted",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_CancelMembership_Id",
                table: "CancelMemberships");

            migrationBuilder.DropIndex(
                name: "IX_CancelMembership_IsActive_IsDeleted",
                table: "CancelMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Blog_IsActive_IsDeleted",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategory_Id",
                table: "BlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategory_IsActive_IsDeleted",
                table: "BlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_User_IsActive_IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_IsActive_IsDeleted",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Blog_Id",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentAnswer_Id",
                table: "AppointmentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentAnswer_IsActive_IsDeleted",
                table: "AppointmentAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_UserSession_UserId",
                table: "UserSessions",
                newName: "IX_UserSessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDo_UserId",
                table: "ToDos",
                newName: "IX_ToDos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileImage_UserId",
                table: "ProfileImages",
                newName: "IX_ProfileImages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubcategory_ProductCategoryId",
                table: "ProductSubcategories",
                newName: "IX_ProductSubcategories_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CustomerStatusId",
                table: "Customers",
                newName: "IX_Customers_CustomerStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CompanyId",
                table: "Customers",
                newName: "IX_Customers_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySubcategory_CompanyCategoryId",
                table: "CompanySubcategories",
                newName: "IX_CompanySubcategories_CompanyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyMessage_UserId",
                table: "CompanyMessages",
                newName: "IX_CompanyMessages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyMessage_CompanyId",
                table: "CompanyMessages",
                newName: "IX_CompanyMessages_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountryId",
                table: "Cities",
                newName: "IX_Cities_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CancelMembership_UserId",
                table: "CancelMemberships",
                newName: "IX_CancelMemberships_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CancelMembership_CancelMembershipReasonId",
                table: "CancelMemberships",
                newName: "IX_CancelMemberships_CancelMembershipReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_UserId",
                table: "Appointments",
                newName: "IX_Appointments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_CompanyId",
                table: "Appointments",
                newName: "IX_Appointments_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentAnswer_AppointmentId",
                table: "AppointmentAnswers",
                newName: "IX_AppointmentAnswers_AppointmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
        }
    }
}
