using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Data;
 
namespace dotnetapp.Repositories
{
    public interface IReviewRepo
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(int id, Review review);
        Task<Review> DeleteReviewAsync(int id);
        Task<Review> GetReviewByIdAsync(int id); // Add this method to the interface
 
    }
 
    public class ReviewRepo : IReviewRepo
    {
        private readonly ApplicationDbContext _dbContext;
 
        public ReviewRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
 
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _dbContext.Reviews
            .Include(b => b.User) // Include user data

            .ToListAsync();
        }
 
        public async Task<Review> AddReviewAsync(Review review)
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
 
        public async Task<Review> UpdateReviewAsync(int id, Review review)
        {
            var existingReview = await _dbContext.Reviews.FindAsync(id);
            if (existingReview == null)
            {
                return null;
            }
 
            existingReview.Subject = review.Subject;
            existingReview.Body = review.Body;
 
            await _dbContext.SaveChangesAsync();
            return existingReview;
        }
 
        public async Task<Review> DeleteReviewAsync(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return null;
            }
 
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _dbContext.Reviews.FindAsync(id);
        }
 
    }
}