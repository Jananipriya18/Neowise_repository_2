using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideShare.Migrations
{
    public partial class ini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    RideID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaximumCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.RideID);
                });

            migrationBuilder.CreateTable(
                name: "Commuters",
                columns: table => new
                {
                    CommuterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RideID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commuters", x => x.CommuterID);
                    table.ForeignKey(
                        name: "FK_Commuters_Rides_RideID",
                        column: x => x.RideID,
                        principalTable: "Rides",
                        principalColumn: "RideID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "RideID", "DateOfDeparture", "DepartureLocation", "Destination", "MaximumCapacity" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6082), "Location1", "Destination1", 4 },
                    { 2, new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6107), "Location2", "Destination2", 3 },
                    { 3, new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6110), "Location3", "Destination3", 2 },
                    { 4, new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6113), "Location4", "Destination4", 4 },
                    { 5, new DateTime(2024, 4, 22, 21, 36, 0, 209, DateTimeKind.Local).AddTicks(6115), "Location5", "Destination5", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commuters_RideID",
                table: "Commuters",
                column: "RideID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commuters");

            migrationBuilder.DropTable(
                name: "Rides");
        }
    }
}
