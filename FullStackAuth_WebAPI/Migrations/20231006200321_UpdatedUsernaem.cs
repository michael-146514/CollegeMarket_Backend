using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUsernaem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03f8fd79-ac26-4c49-bc1a-96220c27e5a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f00ff0b-689f-4452-95b4-f12994ee8a36");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Messages",
                type: "longtext",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9617381a-ccaf-4e9e-8c23-038d47bdaa5a", null, "User", "USER" },
                    { "b8324b52-3400-49de-9068-acfbec1235a2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9617381a-ccaf-4e9e-8c23-038d47bdaa5a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8324b52-3400-49de-9068-acfbec1235a2");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Messages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03f8fd79-ac26-4c49-bc1a-96220c27e5a9", null, "User", "USER" },
                    { "0f00ff0b-689f-4452-95b4-f12994ee8a36", null, "Admin", "ADMIN" }
                });
        }
    }
}
