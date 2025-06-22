using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoginConfirmCode",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LoginConfirmCode",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLoginConfirmCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LoginConfirmCode",
                table: "AspNetUsers");
        }
    }
}
