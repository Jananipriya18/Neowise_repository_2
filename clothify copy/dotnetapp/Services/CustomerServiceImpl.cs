// Services/CustomerServiceImpl.cs
using System;
using dotnetapp.Models;
using dotnetapp.Repositories;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class CustomerServiceImpl : CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ApplicationDbContext _context;

        public CustomerServiceImpl(CustomerRepository customerRepository, ApplicationDbContext context)
        {
            _customerRepository = customerRepository;
            _context = context;
        }

        public Customer registerCustomer(Customer customer)
        {
            // Perform any necessary validation or business logic
            // before calling the repository method.
            
            return _customerRepository.registerCustomer(customer);
        }

        public Customer viewCustomerById(long customerId)
        {
            // Perform any necessary validation or business logic
            // before calling the repository method.

            var customer = _customerRepository.IncludeUser()
                                              .FirstOrDefault(c => c.CustomerId == customerId);

            return customer;
        }

         public Customer getCustomerById(long customerId)
        {
            // Implement the logic to retrieve a customer by ID
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }
    
    }
}
