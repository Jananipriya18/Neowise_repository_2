using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class InitialSetUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turfs",
                columns: table => new
                {
                    TurfID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turfs", x => x.TurfID);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurfID = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Turfs_TurfID",
                        column: x => x.TurfID,
                        principalTable: "Turfs",
                        principalColumn: "TurfID");
                });

            migrationBuilder.InsertData(
                table: "Turfs",
                columns: new[] { "TurfID", "Availability", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, true, 4, "Green Cricket Meadow" },
                    { 2, true, 6, "Sunny Football Fields" },
                    { 3, true, 2, "Golden Golf Garden" },
                    { 4, true, 10, "Silver Tennis Oasis" },
                    { 5, true, 2, "Blue Basketball Arena" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TurfID",
                table: "Bookings",
                column: "TurfID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Turfs");
        }
    }
}
