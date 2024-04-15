using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
 
namespace dotnetapp.Controllers
{
    [Route("api/review")]
    [ApiController]
    

    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
 
        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "admin,customer")]      
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving reviews: {ex.Message}");
            }
        }
 
        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            var addedReview = await _reviewService.AddReviewAsync(review);
            return Ok(addedReview);
        }
 
        [Authorize(Roles = "customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            try
            {
                var updatedReview = await _reviewService.UpdateReviewAsync(id, review);
                if (updatedReview == null)
                {
                    return NotFound();
                }
                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the review: {ex.Message}");
            }
        }
 
        [Authorize(Roles = "customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var deletedReview = await _reviewService.DeleteReviewAsync(id);
                if (deletedReview == null)
                {
                    return NotFound();
                }
                return Ok(deletedReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the review: {ex.Message}");
            }
        }
    }
}