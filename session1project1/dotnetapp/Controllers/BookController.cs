using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string title)
        {
            IQueryable<Book> booksQuery = _context.Books;

            if (!string.IsNullOrEmpty(title))
            {
                title = title.Trim();
                booksQuery = booksQuery.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            List<Book> books = booksQuery.ToList();
            ViewBag.TotalCount = _context.Books.Count();

            return View(books);
        }

        [HttpGet]
        public IActionResult Search(string title)
        {
            IQueryable<Book> booksQuery = _context.Books;

            if (!string.IsNullOrEmpty(title))
            {
                title = title.Trim();
                booksQuery = booksQuery.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            List<Book> books = booksQuery.ToList();
            ViewBag.TotalCount = _context.Books.Count();

            return View("Index", books);
        }

        public IActionResult TotalCount()
        {
            int totalCount = _context.Books.Count();
            return Content($"Total number of books: {totalCount}");
        }
    }
}
