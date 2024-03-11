using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroceryItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroceryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GroceryItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceryItem>>> GetGroceryItems()
        {
            // Retrieve all GroceryItems from the database
            return await _context.GroceryItems.ToListAsync();
        }

        // POST: api/GroceryItem
        [HttpPost]
        public async Task<ActionResult<GroceryItem>> PostGroceryItem(GroceryItem groceryItem)
        {
            // Add a new GroceryItem to the database
            _context.GroceryItems.Add(groceryItem);
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the new GroceryItem
            return CreatedAtAction(nameof(GetGroceryItems), new { id = groceryItem.ItemId }, groceryItem);
        }
    }
}
