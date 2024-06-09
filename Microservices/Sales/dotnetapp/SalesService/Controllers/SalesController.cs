using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesService.Models;
using SalesService.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSales()
        {
            Console.WriteLine("Gateway Timeout");
            return await _context.Sales.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Sales>> CreateSales(Sales sales)
        {
            _context.Sales.Add(sales);
            await _context.SaveChangesAsync();

            // Constructing custom JSON response
            var jsonResponse = new
            {
                status = "success",
                message = "Sales created successfully"
            };
            return CreatedAtAction(nameof(CreateSales), jsonResponse);
        }
    }
}


