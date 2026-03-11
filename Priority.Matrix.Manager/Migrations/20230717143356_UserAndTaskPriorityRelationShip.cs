using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations
{
    public partial class UserAndTaskPriorityRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09326888-e6f4-4ca2-964b-c0e79e11485b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bf5cb10-a344-4cc8-a2b0-0f3107ec697e");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TaskPriorities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5682863f-9cac-4e67-87d1-c2bcd7c48e5e", "898802e8-8849-4dfd-a577-acc1154d14bb", "Manager", "MANAGER" },
                    { "daec07f6-e863-4e67-81e6-81183e304aba", "2836b467-83f9-406f-98e9-0153639f944c", "Administrator", "ADMINISTRATOR" }
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

            migrationBuilder.CreateIndex(
                name: "IX_TaskPriorities_UserId",
                table: "TaskPriorities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskPriorities_AspNetUsers_UserId",
                table: "TaskPriorities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskPriorities_AspNetUsers_UserId",
                table: "TaskPriorities");

            migrationBuilder.DropIndex(
                name: "IX_TaskPriorities_UserId",
                table: "TaskPriorities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5682863f-9cac-4e67-87d1-c2bcd7c48e5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daec07f6-e863-4e67-81e6-81183e304aba");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskPriorities");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09326888-e6f4-4ca2-964b-c0e79e11485b", "1265dfd7-b5ce-4bdc-b766-a764eaf5cc96", "Manager", "MANAGER" },
                    { "8bf5cb10-a344-4cc8-a2b0-0f3107ec697e", "5aa45c49-6d15-471d-9432-0e416b1778af", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "TaskPriorities",
                keyColumn: "TaskId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TaskToSee" },
                values: new object[] { new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
