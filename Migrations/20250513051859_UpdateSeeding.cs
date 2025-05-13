using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreateAt", "Title" },
                values: new object[,]
                {
                    { 1, "This is my first post in the blog", new DateTime(2025, 5, 13, 8, 10, 14, 484, DateTimeKind.Local).AddTicks(9294), "The first post in the blog" },
                    { 2, "This is my second post in the blog", new DateTime(2025, 5, 13, 8, 10, 14, 486, DateTimeKind.Local).AddTicks(3585), "The second post in the blog" },
                    { 3, "This is my third post in the blog", new DateTime(2025, 5, 13, 8, 10, 14, 486, DateTimeKind.Local).AddTicks(3597), "The third post in the blog" }
                });
        }
    }
}
