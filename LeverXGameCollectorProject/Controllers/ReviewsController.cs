using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>  
        /// Retrieves all reviews.  
        /// </summary>  
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _reviewService.GetAllReviewsAsync());

        /// <summary>  
        /// Retrieves a specific review by ID. 
        /// </summary>  
        /// <param name="gameId">The review's unique ID.</param>  
        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetByGameId(int gameId)
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        /// <summary>  
        /// Retrieves a specific review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reviews = await _reviewService.GetReviewByIdAsync(id);
            return Ok(reviews);
        }

        /// <summary>  
        /// Creates a new review.  
        /// </summary>  
        /// <param name="review">The review data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto review)
        { 
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            var _review = _reviewService.CreateReviewAsync(review);
            return CreatedAtAction(nameof(GetById), new { id = _review.Id }, review);
        }

        /// <summary>  
        /// Updates an existing review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        /// <param name="updatedReview">Updated review data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewDto updatedReview)
        {
            await _reviewService.UpdateReviewAsync(id, updatedReview);

            return NoContent();
        }

        /// <summary>  
        /// Deletes a review by ID. 
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = _reviewService.DeleteReviewAsync(id);
            if (review == null) return NotFound();

            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
