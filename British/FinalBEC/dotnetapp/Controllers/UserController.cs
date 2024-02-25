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
        // private readonly PaymentService _paymentService;


        public UserController(UserService userService)
        {
            _userService = userService;
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
            // await _paymentService.CreateUser(user);
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        // [Authorize(Roles="Admin,Student")]

        // [HttpPost("student")]
        // public async Task<IActionResult> CreateStudent(Student student)
        // {
        //     await _userService.CreateStudent(student);
        //     return CreatedAtAction(nameof(GetUserById), new { userId = student.UserId }, student);
        // }

        [Authorize(Roles="Student")]

        [HttpPut("student/{id}")]
        public async Task<IActionResult> UpdateUser(long studentId, User user)
        {
            if (studentId != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = await _userService.GetUserById(studentId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateUser(user);

            return NoContent();
        }
        
        [Authorize(Roles="Admin")]

        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteUser(long studentId)
        {
            var existingUser = await _userService.GetUserById(studentId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(studentId);

            return NoContent();
        }

        // [Authorize(Roles="Student")]

        // [HttpPost]
        // public async Task<IActionResult> CreatePayment(Payment payment)
        // {
        //     await _paymentService.CreatePayment(payment);
        //     return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentID }, payment);
        // }

        
        // [Authorize(Roles="Student")]

        // [HttpPost("student/payment")]
        // public async Task<IActionResult> PostStudentPayment(long studentId, Payment payment)
        // {
        //     payment.StudentId = studentId;
        //     await _userService.AddPaymentToStudent(payment);
        //     return CreatedAtAction(nameof(PostStudentPayment), new { studentId }, payment);
        // }


        //  [Authorize]

    //    [HttpPost("student/payment")]
    //     public async Task<IActionResult> PostStudentPayment(long userId, Payment payment)
    //     {
    //         payment.UserId = userId;
    //         await _userService.AddPaymentToStudent(payment);
    //         return CreatedAtAction(nameof(PostStudentPayment), new { userId }, payment);
    //     }
//     [HttpPost("student/payment")]
// public async Task<IActionResult> PostStudentPayment(long userId, Payment payment)
// {
//     try
//     {
//         payment.UserId = userId;
//         await _userService.AddPaymentToStudent(payment);
//         return CreatedAtAction(nameof(PostStudentPayment), new { userId }, payment);
//     }
//     catch (Exception ex)
//     {
//         // Log the exception for debugging
//         Console.WriteLine(ex);
//         return StatusCode(500, "Internal server error");
//     }
// }

//   [Authorize(Roles="Admin,Student")]

//         [HttpGet]
//         public async Task<IActionResult> GetAllPayments()
//         {
//             var payments = await _paymentService.GetAllPayments();
//             return Ok(payments);
//         }

//         [Authorize(Roles="Admin,Student")]

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetPaymentById(int id)
//         {
//             var payment = await _paymentService.GetPaymentById(id);
//             if (payment == null)
//             {
//                 return NotFound();
//             }
//             return Ok(payment);
//         }

//        [Authorize(Roles="Student")]

//         [HttpPost]
//         public async Task<IActionResult> CreatePayment(Payment payment)
//         {
//             await _paymentService.CreatePayment(payment);
//             return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentID }, payment);
//         }
    }
}
