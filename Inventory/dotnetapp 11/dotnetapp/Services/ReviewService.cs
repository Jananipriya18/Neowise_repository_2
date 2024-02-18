using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        // Exclude the Password property
    }
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public List<Review> GetAllReviews()
        {
            var reviewsWithUsers = _context.Reviews
            .Include(r => r.User)
            .Select(r => new Review
            {
                ReviewId = r.ReviewId,
                Subject = r.Subject,
                Body = r.Body,
                Rating = r.Rating,
                DateCreated = r.DateCreated,
                User = new User
                {
                    UserId = r.User.UserId,
                    Email = r.User.Email,
                    Username = r.User.Username,
                    MobileNumber = r.User.MobileNumber
                }
            })
            .ToList();

            return reviewsWithUsers;
        }

        
    }
}
