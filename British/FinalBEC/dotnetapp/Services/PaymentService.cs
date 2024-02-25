// PaymentService.cs
using dotnetapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public interface PaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task CreatePayment(Payment payment);
        Task UpdatePayment(Payment payment);
        Task DeletePayment(int id);
    }
}