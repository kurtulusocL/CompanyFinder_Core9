using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RemoteIpAddress",
                table: "Audits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MacAddress",
                table: "Audits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "Audits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddressVPN",
                table: "Audits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BlackLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MacAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalIpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IpAddressVPN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlackLists_Audits_AuditId",
                        column: x => x.AuditId,
                        principalTable: "Audits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_Id",
                table: "Audits",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audit_IsActive_IsDeleted",
                table: "Audits",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "Audits",
                columns: new[] { "LocalIpAddress", "RemoteIpAddress", "IpAddressVPN" });

            migrationBuilder.CreateIndex(
                name: "IX_Audit_MacAddress",
                table: "Audits",
                column: "MacAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_MacAddress_IsActive_IsDeleted",
                table: "Audits",
                columns: new[] { "MacAddress", "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_AuditId",
                table: "BlackLists",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_ExpirationDate",
                table: "BlackLists",
                column: "ExpirationDate");

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_Id",
                table: "BlackLists",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_IpAddress",
                table: "BlackLists",
                column: "IpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_IsActive_IsDeleted",
                table: "BlackLists",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "BlackLists",
                columns: new[] { "LocalIpAddress", "RemoteIpAddress", "IpAddressVPN" });

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_MacAddress",
                table: "BlackLists",
                column: "MacAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackLists");

            migrationBuilder.DropIndex(
                name: "IX_Audit_Id",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audit_IsActive_IsDeleted",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audit_LocalIpAddress_RemoteIpAddress_IpAddressVPN",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audit_MacAddress",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audit_MacAddress_IsActive_IsDeleted",
                table: "Audits");

            migrationBuilder.AlterColumn<string>(
                name: "RemoteIpAddress",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MacAddress",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LocalIpAddress",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddressVPN",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
