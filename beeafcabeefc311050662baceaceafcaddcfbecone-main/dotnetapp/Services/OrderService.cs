using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class OrderService
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerName = "John Doe", OrderDate = new DateTime(2023, 1, 1), TotalAmount = 100.50m, Status = "Shipped" },
            new Order { OrderId = 2, CustomerName = "Jane Smith", OrderDate = new DateTime(2023, 2, 15), TotalAmount = 250.75m, Status = "Processing" },
            new Order { OrderId = 3, CustomerName = "Alice Johnson", OrderDate = new DateTime(2023, 3, 20), TotalAmount = 150.00m, Status = "Delivered" }
        };

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(p => p.OrderId == orderId);
        }

        public void AddOrder(Order newOrder)
        {
            // Assuming order IDs are unique and generated elsewhere
            _orders.Add(newOrder);
        }
    }
}
