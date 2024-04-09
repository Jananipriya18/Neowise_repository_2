// // // Repositories/CustomerRepository.cs
// // using System;
// // using System.Collections.Generic;
// // using dotnetapp.Models;

// // namespace dotnetapp.Repositories
// // {
// //     public class CustomerRepository
// //     {
// //         private readonly List<Customer> _customers = new List<Customer>();
// //         private long _nextCustomerId = 1;

// //         public Customer registerCustomer(Customer customer)
// //         {
// //             customer.CustomerId = _nextCustomerId++;
// //             _customers.Add(customer);
// //             return customer;
// //         }

// //         public Customer viewCustomerById(long customerId)
// //         {
// //             return _customers.Find(c => c.CustomerId == customerId);
// //         }
// //     }
// // }



// // Repositories/CustomerRepository.cs
// using System.Collections.Generic;
// using System.Linq;
// using dotnetapp.Models;
// using dotnetapp.Data;  // Assuming you have a DbContext in a 'Data' namespace

// namespace dotnetapp.Repositories
// {
//     public class CustomerRepository
//     {
//         private readonly ApplicationDbContext _context;

//         public CustomerRepository(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public Customer registerCustomer(Customer customer)
//         {
//             _context.Customers.Add(customer);
//             _context.SaveChanges();
//             return customer;
//         }

//         public Customer viewCustomerById(long customerId)
//         {
//             return _context.Customers.Find(customerId);
//         }
//     }
// }


// Repositories/CustomerRepository.cs
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Repositories
{
    public class CustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer registerCustomer(Customer customer)
        {
            Console.Write("sss"+customer.CustomerId);
            
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer viewCustomerById(long customerId)
        {

            return _context.Customers.Find(customerId);
        }

        // Include method to eagerly load the User navigation property
        public IQueryable<Customer> IncludeUser()
        {
            return _context.Customers.Include(c => c.User);
        }
    }
}
