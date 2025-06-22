using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorNewsId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorNewsId",
                table: "Hits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SectorNewses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorNewses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SectorNewsId",
                table: "Likes",
                column: "SectorNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Hits_SectorNewsId",
                table: "Hits",
                column: "SectorNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_SectorNews_Id",
                table: "SectorNewses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectorNews_IsActive_IsDeleted",
                table: "SectorNewses",
                columns: new[] { "IsActive", "IsDeleted" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_SectorNewses_SectorNewsId",
                table: "Hits",
                column: "SectorNewsId",
                principalTable: "SectorNewses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_SectorNewses_SectorNewsId",
                table: "Likes",
                column: "SectorNewsId",
                principalTable: "SectorNewses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hits_SectorNewses_SectorNewsId",
                table: "Hits");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_SectorNewses_SectorNewsId",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "SectorNewses");

            migrationBuilder.DropIndex(
                name: "IX_Likes_SectorNewsId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Hits_SectorNewsId",
                table: "Hits");

            migrationBuilder.DropColumn(
                name: "SectorNewsId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "SectorNewsId",
                table: "Hits");
        }
    }
}
