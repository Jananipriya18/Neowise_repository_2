using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class hello : Migration
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
                    { 1, "Location1", new DateTime(2024, 4, 26, 9, 18, 27, 314, DateTimeKind.Local).AddTicks(3957), "Destination1", 4 },
                    { 2, "Location2", new DateTime(2024, 4, 26, 9, 18, 27, 314, DateTimeKind.Local).AddTicks(3973), "Destination2", 3 },
                    { 3, "Location3", new DateTime(2024, 4, 26, 9, 18, 27, 314, DateTimeKind.Local).AddTicks(3974), "Destination3", 2 },
                    { 4, "Location4", new DateTime(2024, 4, 26, 9, 18, 27, 314, DateTimeKind.Local).AddTicks(3975), "Destination4", 4 },
                    { 5, "Location5", new DateTime(2024, 4, 26, 9, 18, 27, 314, DateTimeKind.Local).AddTicks(3976), "Destination5", 3 }
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
