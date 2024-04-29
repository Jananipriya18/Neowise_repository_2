using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class InitialSetUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 1,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 29, 7, 25, 34, 983, DateTimeKind.Local).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 2,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 29, 8, 25, 34, 983, DateTimeKind.Local).AddTicks(3445));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 3,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 29, 14, 25, 34, 983, DateTimeKind.Local).AddTicks(3447));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 4,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 29, 10, 25, 34, 983, DateTimeKind.Local).AddTicks(3448));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 5,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 29, 12, 25, 34, 983, DateTimeKind.Local).AddTicks(3449));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 1,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 27, 15, 16, 31, 349, DateTimeKind.Local).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 2,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 27, 16, 16, 31, 349, DateTimeKind.Local).AddTicks(6854));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 3,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 27, 22, 16, 31, 349, DateTimeKind.Local).AddTicks(6856));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 4,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 27, 18, 16, 31, 349, DateTimeKind.Local).AddTicks(6857));

            migrationBuilder.UpdateData(
                table: "Trains",
                keyColumn: "TrainID",
                keyValue: 5,
                column: "DepartureTime",
                value: new DateTime(2024, 4, 27, 20, 16, 31, 349, DateTimeKind.Local).AddTicks(6858));
        }
    }
}
