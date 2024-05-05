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
    public class TurfBookingControllerTests
    {
        private ApplicationDbContext _dbContext;
        private TurfController _turfController;
        private BookingController _bookingController;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _turfController = new TurfController(_dbContext);
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
        public void BookingController_Get_Book_by_turfId_ReturnsViewResult()
        {
            // Arrange
            var turfId = 1;
            var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
            _dbContext.Turfs.Add(turf);
            _dbContext.SaveChanges();

            // Act
            var result = _bookingController.Book(turfId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BookingController_Get_Book_by_InvalidTurfId_ReturnsNotFound()
        {
            // Arrange
            var turfId = 1;

            // Act
            var result = _bookingController.Book(turfId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BookingController_Post_Book_ValidBooking_Success_Redirects_Details()
        {
            // Arrange
            var turfId = 1;
            var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 7, 30), TimeSlot = TimeSpan.FromHours(10) };
            var reservationDate = new DateTime(2024, 7, 30);
            var timeSlot = TimeSpan.FromHours(10);
            _dbContext.Turfs.Add(turf);
            _dbContext.SaveChanges();

            // Act
            var result = _bookingController.Book(turfId, booking1) as RedirectToActionResult;
            var booking = _dbContext.Bookings.Include(b => b.Turf).FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ActionName);
            Assert.IsNotNull(booking);
            Assert.AreEqual(turfId, booking.Turf.TurfID);
            Assert.AreEqual(booking1.ReservationDate.Date, booking.ReservationDate.Date);
            Assert.AreEqual(booking1.TimeSlot, booking.TimeSlot);
        }

        [Test]
        public void BookingController_Post_Book_by_InvalidTurfId_ReturnsNotFound()
        {
            // Arrange
            var turfId = 1;
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 7, 30), TimeSlot = TimeSpan.FromHours(10) };

            // Act
            var result = _bookingController.Book(turfId, booking1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        // [Test]
        // public void TurfController_Delete_ValidTurfId_Success_Redirects_Index()
        // {
        //     // Arrange
        //     var turfId = 1;
        //     var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
        //     _dbContext.Turfs.Add(turf);
        //     _dbContext.SaveChanges();

        //     // Act
        //     var result = _turfController.Delete(turfId) as RedirectToActionResult;
        //     var deletedTurf = _dbContext.Turfs.FirstOrDefault(t => t.TurfID == turfId);

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual("Index", result.ActionName);
        //     Assert.IsNull(deletedTurf); // Check that the turf has been deleted from the database
        // }

        [Test]
        public void TurfController_Delete_InvalidTurfId_NotFound()
        {
            // Arrange
            var invalidTurfId = 999;

            // Act
            var result = _turfController.Delete(invalidTurfId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TurfController_Index_ReturnsViewWithTurfList()
        {
            var turfId = 1;
            var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
            _dbContext.Turfs.Add(turf);
            _dbContext.SaveChanges();
            // Act
            var result = _turfController.Index() as ViewResult;
            var model = result?.Model as List<Turf>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model?.Count);
        }

        [Test]
        public void BookingController_Post_Book_by_InvalidReservationDate_ThrowsException()
        {
            // Arrange
            var turfId = 1;
            var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 1, 1), TimeSlot = TimeSpan.FromHours(10) };
            var reservationDate = new DateTime(2023, 1, 1);
            var timeSlot = TimeSpan.FromHours(10);
            _dbContext.Turfs.Add(turf);
            _dbContext.SaveChanges();

            // Act & Assert
            Assert.Throws<TurfBookingException>(() =>
            {
                _bookingController.Book(turfId, booking1);
            });
        }

        [Test]
        public void BookingController_Post_Book_by_InvalidReservationDate_ThrowsException_with_message()
        {
            // Arrange
            var turfId = 1;
            var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
            var booking1 = new Booking { ReservationDate = new DateTime(2024, 1, 1), TimeSlot = TimeSpan.FromHours(10) };

            _dbContext.Turfs.Add(turf);
            _dbContext.SaveChanges();

            // Act & Assert
            var msg = Assert.Throws<TurfBookingException>(() =>
            {
                _bookingController.Book(turfId, booking1);
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
        public void Booking_Properties_TurfID_GetSetCorrectly()
        {
            // Arrange
            var booking = new Booking();

            // Act
            booking.TurfID = 2;

            // Assert
            Assert.AreEqual(2, booking.TurfID);
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
        // [Test]
        // public void Booking_Properties_TurfID_HaveCorrectDataTypes()
        // {
        //     // Arrange
        //     var booking = new Booking();

        //     // Assert
        //     Assert.That(booking.TurfID, Is.TypeOf<int>());
        // }

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
        public void TurfClassExists()
        {
            var turf = new Turf();

            Assert.IsNotNull(turf);
        }

        [Test]
        public void BookingClassExists()
        {
            var booking = new Booking();

            Assert.IsNotNull(booking);
        }

        [Test]
        public void ApplicationDbContextContainsDbSetBookingProperty()
        {

            var propertyInfo = _dbContext.GetType().GetProperty("Bookings");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Booking>), propertyInfo.PropertyType);
        }

        [Test]
        public void ApplicationDbContextContainsDbSetTurfProperty()
        {

            var propertyInfo = _dbContext.GetType().GetProperty("Turfs");

            Assert.AreEqual(typeof(DbSet<Turf>), propertyInfo.PropertyType);
        }

        [Test]
        public void Turf_Properties_GetSetCorrectly()
        {
            // Arrange
            var turf = new Turf();

            // Act
            turf.TurfID = 1;
            turf.Name = "Turf 1";

            // Assert
            Assert.AreEqual(1, turf.TurfID);
            Assert.AreEqual("Turf 1", turf.Name);
        }

        [Test]
        public void Turf_Properties_Capacity_GetSetCorrectly()
        {
            // Arrange
            var turf = new Turf();

            turf.Capacity = 4;

            Assert.AreEqual(4, turf.Capacity);
        }

        [Test]
        public void Turf_Properties_Availability_GetSetCorrectly()
        {
            // Arrange
            var turf = new Turf();

            turf.Availability = true;

            Assert.IsTrue(turf.Availability);
        }

        // [Test]
        // public void Turf_Properties_HaveCorrectDataTypes()
        // {
        //     // Arrange
        //     var turf = new Turf();

        //     // Assert
        //     Assert.That(turf.TurfID, Is.TypeOf<int>());
        //     Assert.That(turf.Name, Is.TypeOf<string>());
        //     Assert.That(turf.Capacity, Is.TypeOf<int>());
        //     Assert.That(turf.Availability, Is.TypeOf<bool>());
        // }

        [Test]
public void Booking_Properties_TurfID_HaveCorrectDataTypes()
{
    // Arrange
    var booking = new Booking();

    // Assert
    Assert.That(booking.TurfID, Is.TypeOf<int?>()); // Nullable int
}

[Test]
public void Turf_Properties_HaveCorrectDataTypes()
{
    // Arrange
    var turf = new Turf();

    // Assert
    Assert.That(turf.TurfID, Is.TypeOf<int>());
    Assert.That(turf.Name, Is.TypeOf<string>());
    Assert.That(turf.Capacity, Is.TypeOf<int>());
    Assert.That(turf.Availability, Is.TypeOf<bool>());
}

[Test]
public void TurfController_Delete_ValidTurfId_Success_Redirects_Index()
{
    // Arrange
    var turfId = 1;
    var turf = new Turf { TurfID = turfId, Name = "Turf 1", Capacity = 4, Availability = true };
    _dbContext.Turfs.Add(turf);
    _dbContext.SaveChanges();

    var turfController = new TurfController(_dbContext); // Initialize controller with the correct context

    // Act
    var result = turfController.Delete(turfId) as RedirectToActionResult;
    var deletedTurf = _dbContext.Turfs.FirstOrDefault(t => t.TurfID == turfId);

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("Index", result.ActionName);
    Assert.IsNull(deletedTurf); // Check that the turf has been deleted from the database
}

    }
}
