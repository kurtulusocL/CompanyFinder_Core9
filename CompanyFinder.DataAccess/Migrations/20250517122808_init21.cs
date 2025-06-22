using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hits_CommentAnswers_CommentAnswerId",
                table: "Hits");

            migrationBuilder.DropForeignKey(
                name: "FK_Hits_Comments_CommentId",
                table: "Hits");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CommentAnswers_CommentAnswerId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Comments_CommentId",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "FooterInfos");

            migrationBuilder.DropIndex(
                name: "IX_Like_CommentAnswerId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Like_CommentId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Hit_CommentAnswerId",
                table: "Hits");

            migrationBuilder.DropIndex(
                name: "IX_Hit_CommentId",
                table: "Hits");

            migrationBuilder.DropColumn(
                name: "CommentAnswerId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CommentAnswerId",
                table: "Hits");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Hits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentAnswerId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentAnswerId",
                table: "Hits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Hits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FooterInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SuspendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentAnswerId",
                table: "Likes",
                column: "CommentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CommentAnswerId",
                table: "Hits",
                column: "CommentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hit_CommentId",
                table: "Hits",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_CommentAnswers_CommentAnswerId",
                table: "Hits",
                column: "CommentAnswerId",
                principalTable: "CommentAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hits_Comments_CommentId",
                table: "Hits",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CommentAnswers_CommentAnswerId",
                table: "Likes",
                column: "CommentAnswerId",
                principalTable: "CommentAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Comments_CommentId",
                table: "Likes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
