using dotnetapp.Models;
using dotnetapp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
 
namespace dotnetapp.Services
{
    public class ReviewServiceImpl : ReviewService
    {
        private readonly IReviewRepo _reviewRepo;
 
        public ReviewServiceImpl(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }
 
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepo.GetAllReviewsAsync();
        }
 
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _reviewRepo.GetReviewByIdAsync(id);
        }
 
        public async Task<Review> AddReviewAsync(Review review)
        {
            return await _reviewRepo.AddReviewAsync(review);
        }
 
        public async Task<Review> UpdateReviewAsync(int id, Review review)
        {
            return await _reviewRepo.UpdateReviewAsync(id, review);
        }
 
        public async Task<Review> DeleteReviewAsync(int id)
        {
            return await _reviewRepo.DeleteReviewAsync(id);
        }
    }
}