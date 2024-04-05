using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class BeautySpaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeautySpaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult View()
        {
            var beautySpas = _context.BeautySpa.ToList();
            return View(beautySpas);
        }

        public IActionResult Create()
        {
            return View(new BeautySpa());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BeautySpa beautySpa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beautySpa);
                _context.SaveChanges();
                return RedirectToAction(nameof(View));
            }
            return View(beautySpa);
        }
    }
}
