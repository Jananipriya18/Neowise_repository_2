// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using dotnetapp.Models;
// using dotnetapp.Data;

// namespace dotnetapp.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class RecipeController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;

//         public RecipeController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         // GET: api/Recipe
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
//         {
//             return await _context.Recipes.ToListAsync();
//         }

//         // GET: api/Recipe/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Recipe>> GetRecipe(int id)
//         {
//             var recipe = await _context.Recipes.FindAsync(id);

//             if (recipe == null)
//             {
//                 return NotFound();
//             }

//             return recipe;
//         }

//         // POST: api/Recipe
//         [HttpPost]
//         public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
//         {
//             _context.Recipes.Add(recipe);
//             await _context.SaveChangesAsync();

//             return CreatedAtAction("GetRecipe", new { id = recipe.recipeId }, recipe);
//         }

//         // DELETE: api/Recipe/5
//         [HttpDelete("{id}")]
//         public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
//         {
//             var recipe = await _context.Recipes.FindAsync(id);
//             if (recipe == null)
//             {
//                 return NotFound();
//             }

//             _context.Recipes.Remove(recipe);
//             await _context.SaveChangesAsync();

//             return recipe;
//         }

//     }
// }
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;
using dotnetapp.Data;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LaptopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Laptop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops()
        {
            return await _context.Laptops.ToListAsync();
        }

        // GET: api/Laptop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laptop>> GetLaptop(int id)
        {
            var laptop = await _context.Laptops.FindAsync(id);

            if (laptop == null)
            {
                return NotFound();
            }

            return laptop;
        }

        // POST: api/Laptop
        [HttpPost]
        public async Task<ActionResult<Laptop>> PostLaptop(Laptop laptop)
        {
            _context.Laptops.Add(laptop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaptop", new { id = laptop.laptopId }, laptop);
        }

        // DELETE: api/Laptop/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Laptop>> DeleteLaptop(int id)
        {
            var laptop = await _context.Laptops.FindAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }

            _context.Laptops.Remove(laptop);
            await _context.SaveChangesAsync();

            return laptop;
        }
    }
}
