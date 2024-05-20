using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;
        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        //getAllFeedbacks

        [HttpGet("getAllFeedbacks")]
        public async Task<ActionResult<IEnumerable<Crop>>> GetAllFeedbacks()
        {
            var feedbacks = await _feedbackService.GetAllFeedbacks();
            if (feedbacks == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(feedbacks);
            }
        }
        //getFeedbacksByUserId

        [HttpGet("/getFeedbacksByUserId/:userId")]
        public async Task<ActionResult<Crop>> GetFeedbacksByUserId(int userID)
        {
            var crop = await _feedbackService.GetFeedbacksByUserId(userID);
            if (crop == null)
                return NotFound(new { message = "Cannot Find any FeedBack" });
            return Ok(crop);
        }


        //addFeedback
        [HttpPost("addFeedback")]
        public async Task<ActionResult> AddFeedback([FromBody] Feedback feedback)
        {
            try
            {
                var sucess = await _feedbackService.AddFeedback(feedback);
                if (sucess)
                {
                    return Ok(new { message = "Feedback added sucessfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add Feedback" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
