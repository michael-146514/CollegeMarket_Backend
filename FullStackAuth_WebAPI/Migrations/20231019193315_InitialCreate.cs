using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38e49a13-9380-4070-a2ed-7b04aecfe00e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c29acf7-a237-418c-a24e-0d9cef7aad3c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f095e611-ea95-4b74-9400-a12a3541c00f", null, "Admin", "ADMIN" },
                    { "f0e7467a-d330-4ae0-9409-c25c212f6623", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f095e611-ea95-4b74-9400-a12a3541c00f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0e7467a-d330-4ae0-9409-c25c212f6623");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38e49a13-9380-4070-a2ed-7b04aecfe00e", null, "Admin", "ADMIN" },
                    { "7c29acf7-a237-418c-a24e-0d9cef7aad3c", null, "User", "USER" }
                });
        }
    }
}
