using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Priority.Matrix.Manager.Migrations;

public partial class InitialData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "CategoryId", "CategoryCode", "CategoryName" },
            values: new object[,]
            {
                { 1, "important-et-urgent", "Important et urgent" },
                { 2, "important-mais-pas-urgent ", "Important, mais pas urgent" },
                { 3, "pas-important-mais-urgent", "Important, mais pas urgent" },
                { 4, "Ni-important-ni-urgent", "Ni important, ni urgent" }
            });

        migrationBuilder.InsertData(
            table: "TaskPriorities",
            columns: new[] { "TaskId", "CategoryID", "CreatedDate", "Hour", "TaskCreatedBy", "TaskDescription", "TaskStatus", "TaskTitre", "TaskToSee" },
            values: new object[,]
            {
                { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.", "To do", "Title task 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.", "To do", "Title task 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.", "To do", "Title task 3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 4, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.", "To do", "Title task 4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac turpis egestas sed tempus urna et pharetra pharetra massa.", "To do", "Title task 5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "TaskPriorities",
            keyColumn: "TaskId",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "TaskPriorities",
            keyColumn: "TaskId",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "TaskPriorities",
            keyColumn: "TaskId",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "TaskPriorities",
            keyColumn: "TaskId",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "TaskPriorities",
            keyColumn: "TaskId",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "CategoryId",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "CategoryId",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "CategoryId",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "CategoryId",
            keyValue: 4);
    }
}
