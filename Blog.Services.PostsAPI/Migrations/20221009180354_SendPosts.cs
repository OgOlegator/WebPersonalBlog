using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Services.PostsAPI.Migrations
{
    /// <inheritdoc />
    public partial class SendPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CreatedDate", "Name", "Text" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(255), "Тестовый пост 1", "..." },
                    { 2, new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(295), "Тестовый пост 2", "..." },
                    { 3, new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(305), "Тестовый пост 3", "..." },
                    { 4, new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(314), "Тестовый пост 4", "..." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
