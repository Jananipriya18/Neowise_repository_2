using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class ini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiningTables",
                columns: table => new
                {
                    DiningTableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiningTables", x => x.DiningTableID);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiningTableID = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_DiningTables_DiningTableID",
                        column: x => x.DiningTableID,
                        principalTable: "DiningTables",
                        principalColumn: "DiningTableID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DiningTables",
                columns: new[] { "DiningTableID", "Availability", "SeatingCapacity" },
                values: new object[,]
                {
                    { 1, true, 4 },
                    { 2, true, 6 },
                    { 3, true, 2 },
                    { 4, false, 10 },
                    { 5, true, 2 },
                    { 6, false, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DiningTableID",
                table: "Bookings",
                column: "DiningTableID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "DiningTables");
        }
    }
}
