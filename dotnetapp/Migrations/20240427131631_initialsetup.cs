using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaximumCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.TrainID);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerID);
                    table.ForeignKey(
                        name: "FK_Passengers_Trains_TrainID",
                        column: x => x.TrainID,
                        principalTable: "Trains",
                        principalColumn: "TrainID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "TrainID", "DepartureLocation", "DepartureTime", "Destination", "MaximumCapacity" },
                values: new object[,]
                {
                    { 1, "Delhi", new DateTime(2024, 4, 27, 15, 16, 31, 349, DateTimeKind.Local).AddTicks(6829), "Mumbai", 4 },
                    { 2, "Chennai", new DateTime(2024, 4, 27, 16, 16, 31, 349, DateTimeKind.Local).AddTicks(6854), "Kolkata", 3 },
                    { 3, "Bangalore", new DateTime(2024, 4, 27, 22, 16, 31, 349, DateTimeKind.Local).AddTicks(6856), "Hyderabad", 2 },
                    { 4, "Jaipur", new DateTime(2024, 4, 27, 18, 16, 31, 349, DateTimeKind.Local).AddTicks(6857), "Ahmedabad", 4 },
                    { 5, "Lucknow", new DateTime(2024, 4, 27, 20, 16, 31, 349, DateTimeKind.Local).AddTicks(6858), "Bhopal", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_TrainID",
                table: "Passengers",
                column: "TrainID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
