using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bb61c51-75d5-4ee2-8159-2c98804449c9", "7ff83cd1-1879-4cae-b93a-707dceb51717", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dfbd7277-b04b-4c46-bb23-534e394b4bd4", "b517082f-ed3c-4daa-959d-77e1938d0d7f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bb61c51-75d5-4ee2-8159-2c98804449c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfbd7277-b04b-4c46-bb23-534e394b4bd4");
        }
    }
}
