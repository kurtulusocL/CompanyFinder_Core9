using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdTargets_AdTargetGenders_AdTargetGenderId",
                table: "AdTargets");

            migrationBuilder.DropTable(
                name: "AdTargetGenders");

            migrationBuilder.DropIndex(
                name: "IX_AdTargets_AdTargetGenderId",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "AdTargetGenderId",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "AdTargets");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "AdTargets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdTargetGenderId",
                table: "AdTargets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxAge",
                table: "AdTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "AdTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdTargetGenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdTargetGenders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdTargets_AdTargetGenderId",
                table: "AdTargets",
                column: "AdTargetGenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdTargets_AdTargetGenders_AdTargetGenderId",
                table: "AdTargets",
                column: "AdTargetGenderId",
                principalTable: "AdTargetGenders",
                principalColumn: "Id");
        }
    }
}
