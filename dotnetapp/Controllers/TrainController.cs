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
            var train = _dbContext.Trains.Include(t => t.Passengers).FirstOrDefault(t => t.TrainID == id);
            if (train == null)
            {
                return NotFound();
            }

            int bookedSeats = train.Passengers.Count;
            int availableSeats = train.MaximumCapacity - bookedSeats;

            ViewBag.BookedSeats = bookedSeats;
            ViewBag.AvailableSeats = availableSeats;

            return View(train);
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
