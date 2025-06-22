using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RemoteIpAddress",
                table: "Blockeds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MacAddress",
                table: "Blockeds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "Blockeds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddressVPN",
                table: "Blockeds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "Blockeds",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_IsActive_IsDeleted",
                table: "Blockeds",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Blocked_Id",
                table: "Blockeds",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blocked_IpAddress",
                table: "Blockeds",
                column: "IpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Blocked_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "Blockeds",
                columns: new[] { "LocalIpAddress", "RemoteIpAddress", "IpAddressVPN" });

            migrationBuilder.CreateIndex(
                name: "IX_Blocked_MacAddress",
                table: "Blockeds",
                column: "MacAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlackList_IsActive_IsDeleted",
                table: "Blockeds");

            migrationBuilder.DropIndex(
                name: "IX_Blocked_Id",
                table: "Blockeds");

            migrationBuilder.DropIndex(
                name: "IX_Blocked_IpAddress",
                table: "Blockeds");

            migrationBuilder.DropIndex(
                name: "IX_Blocked_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "Blockeds");

            migrationBuilder.DropIndex(
                name: "IX_Blocked_MacAddress",
                table: "Blockeds");

            migrationBuilder.AlterColumn<string>(
                name: "RemoteIpAddress",
                table: "Blockeds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MacAddress",
                table: "Blockeds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "Blockeds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddressVPN",
                table: "Blockeds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "Blockeds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
