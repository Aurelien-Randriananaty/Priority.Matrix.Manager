using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations
{
    public partial class InitialDataFixDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
