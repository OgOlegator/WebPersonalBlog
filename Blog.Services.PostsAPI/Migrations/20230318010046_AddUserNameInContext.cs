using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Services.PostsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameInContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserName" },
                values: new object[] { new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1862), "TestUser" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UserName" },
                values: new object[] { new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1898), "TestUser" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UserName" },
                values: new object[] { new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1906), "TestUser" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UserName" },
                values: new object[] { new DateTime(2023, 3, 18, 4, 0, 45, 955, DateTimeKind.Local).AddTicks(1956), "TestUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(255));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(295));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(305));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 21, 3, 54, 477, DateTimeKind.Local).AddTicks(314));
        }
    }
}
