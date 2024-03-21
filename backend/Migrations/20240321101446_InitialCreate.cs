using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
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
                keyValue: "6a84c856-f6ff-4719-a088-bee2c258a424");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d9281a3-eb6a-4c92-a067-0525fb146322");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8480428e-e6ca-42dc-ad87-8cabb25aacce", null, "Admin", "ADMIN" },
                    { "ede348ca-b26e-4094-b2fc-7bbc26f5a671", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8480428e-e6ca-42dc-ad87-8cabb25aacce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ede348ca-b26e-4094-b2fc-7bbc26f5a671");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a84c856-f6ff-4719-a088-bee2c258a424", null, "Admin", "ADMIN" },
                    { "9d9281a3-eb6a-4c92-a067-0525fb146322", null, "User", "USER" }
                });
        }
    }
}
