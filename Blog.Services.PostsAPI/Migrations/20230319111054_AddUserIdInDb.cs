using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Services.PostsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Posts",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "UserName");

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreatedDate", "Name", "Text", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1862), "Тестовый пост 1", "...", "TestUser" },
                    { 2, new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1898), "Тестовый пост 2", "...", "TestUser" },
                    { 3, new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1906), "Тестовый пост 3", "...", "TestUser" },
                    { 4, new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1956), "Тестовый пост 4", "...", "TestUser" }
                });
        }
    }
}
