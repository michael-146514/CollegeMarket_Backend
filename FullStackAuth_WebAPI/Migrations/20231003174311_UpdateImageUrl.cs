using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be0ada50-4da1-483d-84b1-7edd9392e0d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfda347f-fd57-4dac-bfde-e4f8f9120b92");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "ImageUrl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93064994-4bab-42c5-a586-a0a99951e56d", null, "User", "USER" },
                    { "99511521-7fdc-4061-a247-9adbbdffeeb5", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93064994-4bab-42c5-a586-a0a99951e56d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99511521-7fdc-4061-a247-9adbbdffeeb5");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "ImageUrl");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "be0ada50-4da1-483d-84b1-7edd9392e0d9", null, "User", "USER" },
                    { "bfda347f-fd57-4dac-bfde-e4f8f9120b92", null, "Admin", "ADMIN" }
                });
        }
    }
}
