using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsappNo",
                table: "CompanyContacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsappNo",
                table: "CompanyContacts");
        }
    }
}
