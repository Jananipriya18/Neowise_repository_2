using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class FeedbackService
    {
        private readonly ApplicationDbContext _context;
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        //getAllFeedbacks
    
        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();

        }
        //getFeedbacksByUserId
     
        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserId(int userId)
        {
            return await _context.Feedbacks.Include(x => x.UserId).ToListAsync();
        }
        //addFeedback
        
        public async Task<bool> AddFeedback(Feedback newFeedback)
        {
            _context.Feedbacks.Add(newFeedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
