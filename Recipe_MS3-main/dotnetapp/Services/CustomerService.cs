// Services/CustomerService.cs
using dotnetapp.Models;

public interface CustomerService
{
    Customer registerCustomer(Customer customer);
    Customer viewCustomerById(long customerId);
    Customer getCustomerById(long customerId);
    
}