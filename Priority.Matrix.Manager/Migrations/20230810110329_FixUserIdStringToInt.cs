using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations
{
    public partial class FixUserIdStringToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82d326aa-042f-40ad-8d4b-5c1cf88a4e66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f006d0d7-061b-461b-9667-9f05f0ae5516");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31e36c25-3971-44a3-b469-f39e595fbec6", "760d511a-e678-4fc4-98c2-1cb966e06158", "Administrator", "ADMINISTRATOR" },
                    { "c7a82309-bf57-4df8-8141-1d01ccfcdb79", "5721c0bc-cc0a-4720-ad4e-d9008292eadd", "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31e36c25-3971-44a3-b469-f39e595fbec6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a82309-bf57-4df8-8141-1d01ccfcdb79");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "82d326aa-042f-40ad-8d4b-5c1cf88a4e66", "f44b577d-7ae8-4c7c-9d3a-a32bb7c17958", "Manager", "MANAGER" },
                    { "f006d0d7-061b-461b-9667-9f05f0ae5516", "9f5ff7ef-7331-4420-ae5d-ac1f923d9438", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
