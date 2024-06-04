using Microsoft.AspNetCore.Mvc;
using dotnetapp.Services;
using dotnetapp.Models;

using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/order
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // GET api/order/{orderId}
        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrderById(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound(); // HTTP 404
            }
            return Ok(order); // HTTP 200
        }

        // POST api/order
        [HttpPost]
        public ActionResult<Order> CreateOrder(Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest(); // HTTP 400
            }
            _orderService.AddOrder(newOrder);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = newOrder.OrderId }, newOrder); // HTTP 201
        }
    }
}
