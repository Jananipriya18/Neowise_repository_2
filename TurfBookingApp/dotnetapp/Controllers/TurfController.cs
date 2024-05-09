using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class TurfController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TurfController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var tables = _dbContext.Turfs.ToList();
            return View(tables);
        }

        public IActionResult Delete(int turfId)
        {
            var turf = _dbContext.Turfs.FirstOrDefault(t => t.TurfID == turfId);
            if (turf == null)
            {
                return NotFound();
            }

            return View(turf);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int turfId)
        {
            var turf = _dbContext.Turfs.FirstOrDefault(t => t.TurfID == turfId);
            if (turf == null)
            {
                return NotFound();
            }

            _dbContext.Turfs.Remove(turf);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
