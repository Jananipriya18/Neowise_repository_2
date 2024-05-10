using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Exceptions;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Book(int turfId)
        {
            var turf = _dbContext.Turfs
                .Include(t => t.Bookings)
                .FirstOrDefault(t => t.TurfID == turfId);

            if (turf == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Book(int turfId, Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(booking);
                }
                
                var turf = _dbContext.Turfs
                    .Include(t => t.Bookings)
                    .FirstOrDefault(t => t.TurfID == turfId);

                if (turf == null)
                {
                    return NotFound();
                }

                // Check if booking date is valid
                DateTime targetDate = new DateTime(2024, 4, 25);
                if (booking.ReservationDate < targetDate)
                {
                    throw new TurfBookingException("Booking starts from 25/4/2024");
                }

                // Assign Turf ID to the booking
                booking.TurfID = turfId;

                // Add booking to the database
                _dbContext.Bookings.Add(booking);
                _dbContext.SaveChanges();

                // Redirect to booking details page
                return RedirectToAction("Details", new { bookingId = booking.BookingID });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw;
            }
        }


        public IActionResult Details(int bookingId)
        {
            var booking = _dbContext.Bookings
                .Include(b => b.Turf)
                .FirstOrDefault(b => b.BookingID == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }
}
