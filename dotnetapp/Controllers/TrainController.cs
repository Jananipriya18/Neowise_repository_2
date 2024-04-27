using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    public class TrainController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TrainController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AvailableTrains()
        {
            var trains = _dbContext.Trains.Include(t => t.Passengers).ToList();
            return View(trains);
        }
    
        public IActionResult Details(int id)
        {
            var passenger = _dbContext.Passengers.Include(p => p.Train).FirstOrDefault(p => p.PassengerID == id);
            if (passenger == null)
            {
                return NotFound();
            }

            // Calculate booked seats and available seats based on the associated train
            int bookedSeats = passenger.Train.Passengers.Count;
            int availableSeats = passenger.Train.MaximumCapacity - bookedSeats;

            ViewBag.BookedSeats = bookedSeats;
            ViewBag.AvailableSeats = availableSeats;

            return View("details", passenger);
        }


        public IActionResult DeleteConfirm(int id)
        {
            var train = _dbContext.Trains.Include(t => t.Passengers).FirstOrDefault(t => t.TrainID == id);
            return View(train);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var train = _dbContext.Trains.Find(id);
            if (train == null)
            {
                return NotFound();
            }

            _dbContext.Trains.Remove(train);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(AvailableTrains));
        }
    }
}
