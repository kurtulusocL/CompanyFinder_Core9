﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSessions_AspNetUsers_UserId1",
                table: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_UserSessions_UserId1",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserSessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserId1",
                table: "UserSessions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSessions_AspNetUsers_UserId1",
                table: "UserSessions",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
