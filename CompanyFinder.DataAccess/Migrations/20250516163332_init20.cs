using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Id",
                table: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_PoruductId",
                table: "Hits",
                newName: "IX_Hit_ProductId");

            migrationBuilder.RenameIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                newName: "IX_User_NormalizedUserName");

            migrationBuilder.RenameIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                newName: "IX_User_NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "AspNetUsers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Id",
                table: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Hit_ProductId",
                table: "Hits",
                newName: "IX_Hit_PoruductId");

            migrationBuilder.RenameIndex(
                name: "IX_User_NormalizedUserName",
                table: "AspNetUsers",
                newName: "UserNameIndex");

            migrationBuilder.RenameIndex(
                name: "IX_User_NormalizedEmail",
                table: "AspNetUsers",
                newName: "EmailIndex");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "AspNetUsers",
                column: "Id",
                unique: true);
        }
    }
}
