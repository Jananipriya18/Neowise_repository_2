using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class EmployeeService
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { EmployeeId = 1, Name = "John Doe", Position = "Manager", Salary = 75000m },
            new Employee { EmployeeId = 2, Name = "Jane Smith", Position = "Developer", Salary = 65000m },
            new Employee { EmployeeId = 3, Name = "Alice Johnson", Position = "Designer", Salary = 70000m }
        };

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public void AddEmployee(Employee newEmployee)
        {
            _employees.Add(newEmployee);
        }
    }
}
