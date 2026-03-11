using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations
{
    public partial class PropertiesPositionAndZindexInTaskPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31e36c25-3971-44a3-b469-f39e595fbec6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a82309-bf57-4df8-8141-1d01ccfcdb79");

            migrationBuilder.AddColumn<float>(
                name: "PosX",
                table: "TaskPriorities",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PosY",
                table: "TaskPriorities",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZIndex",
                table: "TaskPriorities",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "67cb37ff-530c-4866-a518-cba3c33ab106", "640cd992-a84b-4a2a-b5ce-25d60523d18c", "Administrator", "ADMINISTRATOR" },
                    { "d64392cb-a67b-4b20-87c4-a3df4796fb6e", "78d15e91-9916-4b6d-b152-980a1e3ee2ec", "Manager", "MANAGER" }
                });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67cb37ff-530c-4866-a518-cba3c33ab106");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d64392cb-a67b-4b20-87c4-a3df4796fb6e");

            migrationBuilder.DropColumn(
                name: "PosX",
                table: "TaskPriorities");

            migrationBuilder.DropColumn(
                name: "PosY",
                table: "TaskPriorities");

            migrationBuilder.DropColumn(
                name: "ZIndex",
                table: "TaskPriorities");

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
    }
}
