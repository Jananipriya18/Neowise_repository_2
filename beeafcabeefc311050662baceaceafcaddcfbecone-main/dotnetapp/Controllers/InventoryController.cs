using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController()
        {
            _inventoryService = new InventoryService();
        }

        // GET: api/inventory
        [HttpGet]
        public ActionResult<IEnumerable<InventoryItem>> GetAllItems()
        {
            var items = _inventoryService.GetAllItems();
            if (items == null || !items.Any())
            {
                return NoContent();
            }
            return Ok(items);
        }

        // GET: api/inventory/{itemId}
        [HttpGet("{itemId}")]
        public ActionResult<InventoryItem> GetItemById(int itemId)
        {
            var item = _inventoryService.GetItemById(itemId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/inventory
        [HttpPost]
        public ActionResult CreateItem([FromBody] InventoryItem newItem)
        {
            if (newItem == null)
            {
                return BadRequest();
            }

            _inventoryService.AddItem(newItem);
            return CreatedAtAction(nameof(GetItemById), new { itemId = newItem.ItemId }, newItem);
        }
    }
}
