using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserBoolAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2abf925c-8150-4534-a6b3-12cc3fc867ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad2ec445-b6f3-471e-ade5-d985bba678bb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d9d1bed-6b06-479b-b0a3-cb8e80ee1e6b", null, "User", "USER" },
                    { "4d4f84c3-ef66-4c43-9b01-3c7f5f56b552", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d9d1bed-6b06-479b-b0a3-cb8e80ee1e6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d4f84c3-ef66-4c43-9b01-3c7f5f56b552");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2abf925c-8150-4534-a6b3-12cc3fc867ad", null, "Admin", "ADMIN" },
                    { "ad2ec445-b6f3-471e-ade5-d985bba678bb", null, "User", "USER" }
                });
        }
    }
}
