using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMessages_Companies_CompanyId",
                table: "CustomerMessages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerMessages_CompanyId",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CustomerMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "CustomerMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMessages_CompanyId",
                table: "CustomerMessages",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMessages_Companies_CompanyId",
                table: "CustomerMessages",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
