using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models; // Assuming your Plant model is in this namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Import Entity Framework Core namespace

namespace dotnetapp.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/plants")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class PlantController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Replace ApplicationDbContext with your actual DbContext

        public PlantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var plants = await _context.Plants.ToListAsync();
            return Ok(plants);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Plant plant)
        {
            if (plant == null)
            {
                return BadRequest("Plant object is null");
            }

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = plant.PlantId }, plant);
        }
    }
}
