using dotnetapp.Models;
 
namespace dotnetapp.Services
{
 
public interface IOrderService
{
    Order AddOrder(Order order);
    List<Order> GetAllOrders();
    Order GetOrderById(long orderId);
    // Order DeleteOrder(long orderId);
    List<Order> GetOrdersByCustomerId(long customerId);
}
}