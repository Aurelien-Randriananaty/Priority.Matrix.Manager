using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations
{
    public partial class IdentityUserChangeColumnNameIdToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5682863f-9cac-4e67-87d1-c2bcd7c48e5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daec07f6-e863-4e67-81e6-81183e304aba");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUsers",
                newName: "UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82d326aa-042f-40ad-8d4b-5c1cf88a4e66", "f44b577d-7ae8-4c7c-9d3a-a32bb7c17958", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f006d0d7-061b-461b-9667-9f05f0ae5516", "9f5ff7ef-7331-4420-ae5d-ac1f923d9438", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82d326aa-042f-40ad-8d4b-5c1cf88a4e66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f006d0d7-061b-461b-9667-9f05f0ae5516");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5682863f-9cac-4e67-87d1-c2bcd7c48e5e", "898802e8-8849-4dfd-a577-acc1154d14bb", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "daec07f6-e863-4e67-81e6-81183e304aba", "2836b467-83f9-406f-98e9-0153639f944c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
