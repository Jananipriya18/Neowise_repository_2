using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PassengerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult BookSeat(int trainId)
        {
            var train = _dbContext.Trains.Include(t => t.Passengers).FirstOrDefault(t => t.TrainID == trainId);
            if (train == null)
            {
                return NotFound();
            }

            int bookedSeats = train.Passengers.Count;
            if (bookedSeats >= train.MaximumCapacity)
            {
                // Handle maximum capacity reached
                throw new Exception("Maximum capacity reached");
            }

            ViewBag.TrainId = trainId;

            return View();
        }

        [HttpPost]
        public IActionResult BookSeat(int trainId, Passenger passenger)
        {
            var train = _dbContext.Trains.Include(t => t.Passengers).FirstOrDefault(t => t.TrainID == trainId);
            if (train == null)
            {
                return NotFound();
            }

            int bookedSeats = train.Passengers.Count;
            if (bookedSeats >= train.MaximumCapacity)
            {
                // Handle maximum capacity reached
                throw new Exception("Maximum capacity reached");
            }

            passenger.TrainID = trainId;

            if (ModelState.IsValid)
            {
                _dbContext.Passengers.Add(passenger);
                _dbContext.SaveChanges();

                return RedirectToAction("Details", "Train", new { id = trainId });
            }

            return View(passenger);
        }
    }
}
