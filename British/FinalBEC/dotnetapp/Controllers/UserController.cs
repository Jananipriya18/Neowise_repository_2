using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using dotnetapp.Repository;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PaymentService _paymentService;

        public UserController(UserService userService, PaymentService paymentService)
        {
            _userService = userService;
            _paymentService = paymentService;
        }

        [Authorize(Roles="Admin")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [Authorize(Roles="Student")]
        [HttpPost("student")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        [Authorize(Roles="Student")]
        [HttpPut("student/{id}")]
        public async Task<IActionResult> UpdateUser(long id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = await _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateUser(user);

            return NoContent();
        }
        
        [Authorize(Roles="Admin")]
        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var existingUser = await _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(id);

            return NoContent();
        }

        [Authorize(Roles="Student")]
        [HttpPost("payment")]
        public async Task<IActionResult> CreatePayment(Payment payment)
        {
            await _paymentService.CreatePayment(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentID }, payment);
        }

        [Authorize(Roles="Admin,Student")]
        [HttpGet("payments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            return Ok(payments);
        }

        [Authorize(Roles="Admin,Student")]
        [HttpGet("payment/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
    }
}
