// Controllers/CustomerController.cs

using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using dotnetapp.Data;
using Microsoft.AspNetCore.Authorization;


namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        [HttpPost]
        public IActionResult registerCustomer([FromBody] Customer customer)
        {
            var registeredCustomer = _customerService.registerCustomer(customer);
            Console.WriteLine("cusid"+registeredCustomer.CustomerId);
            var newCart = new Cart
            {
                CustomerId = customer.CustomerId,
                // Gifts = new List<Gift>(), // Assuming Gifts is initialized as an empty list
                TotalAmount = 0
            };

            _context.Carts.Add(newCart);
            _context.SaveChanges();
            Console.WriteLine("dgfh"+newCart.CartId);
return Ok(new { RegisteredCustomer = registeredCustomer, CartId = newCart.CartId });
            // return CreatedAtAction(nameof(registerCustomer), new { id = registeredCustomer.CustomerId, cartId = newCart.CartId }, registeredCustomer);
        }

        [HttpGet("{customerId}")]
        public IActionResult viewCustomerById(long customerId)
        {
            var customer = _customerService.viewCustomerById(customerId);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found." });
            }

            return Ok(customer);
        }
        
    }
}
