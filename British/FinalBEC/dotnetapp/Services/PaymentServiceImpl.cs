// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using dotnetapp.Models;
// using Microsoft.EntityFrameworkCore;

// namespace dotnetapp.Services
// {
//     public class PaymentService
//     {
//         private readonly ApplicationDbContext _context;

//         public PaymentService(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<IEnumerable<Payment>> GetAllPayments()
//         {
//             return await _context.Payments.ToListAsync();
//         }

//         public async Task CreatePayment(Payment payment)
//         {
//             _context.Payments.Add(payment);
//             await _context.SaveChangesAsync();
//         }

//         public async Task<Payment> GetPaymentById(int id)
//         {
//             return await _context.Payments.FirstOrDefaultAsync(p => p.PaymentID == id);
//         }
//     }
// }


// PaymentServiceImpl.cs
using dotnetapp.Models;
using dotnetapp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class PaymentServiceImpl : PaymentService
    {
        private readonly PaymentRepo _paymentRepository;

        public PaymentServiceImpl(PaymentRepo paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllPayments();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }

        public async Task CreatePayment(Payment payment)
        {
            await _paymentRepository.CreatePayment(payment);
        }

        public async Task UpdatePayment(Payment payment)
        {
            await _paymentRepository.UpdatePayment(payment);
        }

        public async Task DeletePayment(int id)
        {
            await _paymentRepository.DeletePayment(id);
        }
    }
}