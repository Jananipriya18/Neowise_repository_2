using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using dotnetapp.Controllers;
using dotnetapp.Models;
using dotnetapp.Exceptions;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class TableControllerTests
    {
        private ApplicationDbContext _dbContext;
        private TableController _tableController;
        private BookingController _bookingController;


        [SetUp]
        public void Setup()
        {
            // Initialize a new in-memory ApplicationDbContext for testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _tableController = new TableController(_dbContext);
            _bookingController = new BookingController(_dbContext);

        }

        [TearDown]
        public void TearDown()
        {
            // Dispose the ApplicationDbContext and reset the database
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public void BookingController_Get_Book_by_tableId_ReturnsViewResult()
        {
            // Arrange
            var tableId = 1;
            var table = new DiningTable { DiningTableID = tableId, SeatingCapacity = 4, Availability = true };
            _dbContext.DiningTables.Add(table);
            _dbContext.SaveChanges();

            // Act
            var result = _bookingController.Book(tableId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BookingController_Get_Book_by_InvalidTableId_ReturnsNotFound()
        {
            // Arrange
            var tableId = 1;

            // Act
            var result = _bookingController.Book(tableId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BookingController_Post_Book_ValidBooking_Success_Redirects_Details()
        {
            // Arrange
            var tableId = 1;
            var table = new DiningTable { DiningTableID = tableId, SeatingCapacity = 4, Availability = true };
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 7, 30), TimeSlot = TimeSpan.FromHours(10) };
            var reservationDate = new DateTime(2024, 7, 30);
            var timeSlot = TimeSpan.FromHours(10);
            _dbContext.DiningTables.Add(table);
            _dbContext.SaveChanges();

            // Act
            var result = _bookingController.Book(tableId, booking1) as RedirectToActionResult;
            var booking = _dbContext.Bookings.Include(b => b.DiningTable).FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ActionName);
            Assert.IsNotNull(booking);
            Assert.AreEqual(tableId, booking.DiningTable.DiningTableID);
            Assert.AreEqual(booking1.ReservationDate.Date, booking.ReservationDate.Date);
            Assert.AreEqual(booking1.TimeSlot, booking.TimeSlot);
            //Assert.IsFalse(booking.DiningTable.Availability);
        }

        [Test]
        public void BookingController_Post_Book_by_InvalidTableId_ReturnsNotFound()
        {
            // Arrange
            var tableId = 1;
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 7, 30), TimeSlot = TimeSpan.FromHours(10) };


            // Act
            var result = _bookingController.Book(tableId, booking1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TableController_Delete_ValidTableId_Success_Redirects_Index()
        {
            // Arrange
            var tableId = 1;
            var table = new DiningTable { DiningTableID = tableId, SeatingCapacity = 4, Availability = true };
            _dbContext.DiningTables.Add(table);
            _dbContext.SaveChanges();

            // Act
            var result = _tableController.Delete(tableId) as RedirectToActionResult;
            var deletedTable = _dbContext.DiningTables.FirstOrDefault(t => t.DiningTableID == tableId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsNull(deletedTable); // Check that the table has been deleted from the database
        }

        [Test]
        public void TableController_Delete_InvalidTableId_NotFound()
        {
            // Arrange
            var invalidTableId = 999;

            // Act
            var result = _tableController.Delete(invalidTableId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TableController_Index_ReturnsViewWithTableList()
        {
            var tableId = 1;
            var table = new DiningTable { DiningTableID = tableId, SeatingCapacity = 4, Availability = true };
            _dbContext.DiningTables.Add(table);
            _dbContext.SaveChanges();
            // Act
            var result = _tableController.Index() as ViewResult;
            var model = result?.Model as List<DiningTable>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model?.Count);
        }

        [Test]
        public void BookingController_Post_Book_by_InvalidReservationDate_ThrowsException()
        {
            // Arrange
            var tableId = 1;
            var table = new DiningTable { DiningTableID = tableId, SeatingCapacity = 4, Availability = true };
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 1, 1), TimeSlot = TimeSpan.FromHours(10) };
            var reservationDate = new DateTime(2023, 1, 1);
            var timeSlot = TimeSpan.FromHours(10);
            _dbContext.DiningTables.Add(table);
            _dbContext.SaveChanges();

            // Act & Assert
            Assert.Throws<TableBookingException>(() =>
            {
                _bookingController.Book(tableId, booking1);
            });
        }

        [Test]
        public void BookingController_Post_Book_by_InvalidReservationDate_ThrowsException_with_message()
        {
            // Arrange
            var tableId = 1;
            var table = new DiningTable { DiningTableID = tableId, SeatingCapacity = 4, Availability = true };
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 1, 1), TimeSlot = TimeSpan.FromHours(10) };

            _dbContext.DiningTables.Add(table);
            _dbContext.SaveChanges();

            // Act & Assert
            var msg = Assert.Throws<TableBookingException>(() =>
            {
                _bookingController.Book(tableId, booking1);
            });
            Assert.AreEqual("Booking Starts from 25/4/2024", msg.Message);
        }


        [Test]
        public void BookingController_Details_by_InvalidBookingId_ReturnsNotFound()
        {
            // Arrange
            var bookingId = 1;

            // Act
            var result = _bookingController.Details(bookingId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Booking_Properties_BookingID_GetSetCorrectly()
        {
            // Arrange
            var booking = new Booking();

            // Act
            booking.BookingID = 1;

            // Assert
            Assert.AreEqual(1, booking.BookingID);
        }
        [Test]
        public void Booking_Properties_DiningTableID_GetSetCorrectly()
        {
            // Arrange
            var booking = new Booking();

            // Act
            booking.DiningTableID = 2;

            // Assert
            Assert.AreEqual(2, booking.DiningTableID);
        }

        [Test]
        public void Booking_Properties_ReservationDate_GetSetCorrectly()
        {
            // Arrange
            var booking = new Booking();

            booking.ReservationDate = new DateTime(2023, 7, 1);

            Assert.AreEqual(new DateTime(2023, 7, 1), booking.ReservationDate);
        }

        [Test]
        public void Booking_Properties_TimeSlot_GetSetCorrectly()
        {
            // Arrange
            var booking = new Booking();

            booking.TimeSlot = new TimeSpan(14, 0, 0);

            Assert.AreEqual(new TimeSpan(14, 0, 0), booking.TimeSlot);
        }

        [Test]
        public void Booking_Properties_BookingID_HaveCorrectDataTypes()
        {
            // Arrange
            var booking = new Booking();

            // Assert
            Assert.That(booking.BookingID, Is.TypeOf<int>());
        }
        [Test]
        public void Booking_Properties_DiningTableID_HaveCorrectDataTypes()
        {
            // Arrange
            var booking = new Booking();

            // Assert
            Assert.That(booking.DiningTableID, Is.TypeOf<int>());
        }

        [Test]
        public void Booking_Properties_ReservationDate_HaveCorrectDataTypes()
        {
            // Arrange
            var booking = new Booking();
            Assert.That(booking.ReservationDate, Is.TypeOf<DateTime>());
        }

        [Test]
        public void Booking_Properties_TimeSlot_HaveCorrectDataTypes()
        {
            // Arrange
            var booking = new Booking();

            Assert.That(booking.TimeSlot, Is.TypeOf<TimeSpan>());
        }


        [Test]
        public void DiningTableClassExists()
        {
            var diningTable = new DiningTable();

            Assert.IsNotNull(diningTable);
        }

        [Test]
        public void BookingClassExists()
        {
            var booking = new Booking();

            Assert.IsNotNull(booking);
        }

        [Test]
        public void ApplicationDbContextContainsDbSetSlotProperty()
        {

            var propertyInfo = _dbContext.GetType().GetProperty("Bookings");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Booking>), propertyInfo.PropertyType);
            // }
        }



        [Test]
        public void ApplicationDbContextContainsDbSetBookingProperty()
        {

            var propertyInfo = _dbContext.GetType().GetProperty("DiningTables");

            Assert.AreEqual(typeof(DbSet<DiningTable>), propertyInfo.PropertyType);
        }

        [Test]
        public void DiningTable_Properties_GetSetCorrectly()
        {
            // Arrange
            var diningTable = new DiningTable();

            // Act
            diningTable.DiningTableID = 1;
            diningTable.SeatingCapacity = 4;

            // Assert
            Assert.AreEqual(1, diningTable.DiningTableID);
            Assert.AreEqual(4, diningTable.SeatingCapacity);
        }

        [Test]
        public void DiningTable_Properties_Availability_GetSetCorrectly()
        {
            // Arrange
            var diningTable = new DiningTable();

            diningTable.Availability = true;

            Assert.IsTrue(diningTable.Availability);
        }

        [Test]
        public void DiningTable_Properties_HaveCorrectDataTypes()
        {
            // Arrange
            var diningTable = new DiningTable();

            // Assert
            Assert.That(diningTable.DiningTableID, Is.TypeOf<int>());
            Assert.That(diningTable.SeatingCapacity, Is.TypeOf<int>());
            Assert.That(diningTable.Availability, Is.TypeOf<bool>());
        }



    }
}
