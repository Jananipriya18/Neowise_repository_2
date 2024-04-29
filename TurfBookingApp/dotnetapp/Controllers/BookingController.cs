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

                DateTime targetDate = new DateTime(2024, 4, 25);

                if (booking.ReservationDate < targetDate)
                {
                    throw new TurfBookingException("Booking starts from 25/4/2024");
                }
                
                // Check if there are available seats
                if (!turf.Availability)
                {
                    // If there are no available seats, set availability to false
                    turf.Availability = false;
                    _dbContext.SaveChanges(); // Save the changes to the database
                    throw new Exception("No available seats in this turf. It is now under maintenance.");
                }

                booking.TurfID = turfId;

                if (ModelState.IsValid)
                {
                    // Update seating capacity
                    turf.Capacity--;

                    // If seating capacity becomes zero, change availability to false
                    if (turf.Capacity == 0)
                    {
                        turf.Availability = false;
                    }

                    _dbContext.Bookings.Add(booking);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Details", new { bookingId = booking.BookingID });
                }
                return View(booking);
            }
            catch (Exception ex)
            {
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
