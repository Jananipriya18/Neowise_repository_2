using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        // GET: api/employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            if (employees == null || !employees.Any())
            {
                return NoContent();
            }
            return Ok(employees);
        }

        // GET: api/employee/{employeeId}
        [HttpGet("{employeeId}")]
        public ActionResult<Employee> GetEmployeeById(int employeeId)
        {
            var employee = _employeeService.GetEmployeeById(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public ActionResult CreateEmployee([FromBody] Employee newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest();
            }

            _employeeService.AddEmployee(newEmployee);
            return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = newEmployee.EmployeeId }, newEmployee);
        }
    }
}
