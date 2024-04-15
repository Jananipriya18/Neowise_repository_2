using System;
using dotnetapp.Models;
using dotnetapp.Repositories;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
 
namespace dotnetapp.Services
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly OrderRepository _orderRepository; // Assuming OrderRepository is the concrete repository
 
        public OrderServiceImpl(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
 
        public Order AddOrder(Order order)
        {
            // Additional business logic, if needed
            return _orderRepository.AddOrder(order);
        }
 
        public List<Order> GetAllOrders()
        {
            // Additional business logic, if needed
            return _orderRepository.GetAllOrders();
        }
 
        public Order GetOrderById(long orderId)
        {
            // Additional business logic, if needed
            return _orderRepository.GetOrderById(orderId);
        }
 
        // public Order DeleteOrder(long orderId)
        // {
        //     // Additional business logic, if needed
        //     return _orderRepository.DeleteOrder(orderId);
        // }
 
        public List<Order> GetOrdersByCustomerId(long customerId)
        {
            // Additional business logic, if needed
            return _orderRepository.GetOrdersByCustomerId(customerId);
        }
    }
}