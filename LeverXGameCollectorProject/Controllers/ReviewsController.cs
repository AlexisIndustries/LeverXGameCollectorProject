using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private static List<Review> _reviews = new() 
        {
            new Review
            {
                Id = 1,
                GameId = 1,
                ReviewerName = "Alex",
                Rating = 5,
                Comment = "Masterpiece!",
                ReviewDate = new DateTime(2024, 1, 10)
            },
            new Review
            {
                Id = 2,
                GameId = 2,
                ReviewerName = "Maria",
                Rating = 4,
                Comment = "Fun but too short",
                ReviewDate = new DateTime(2024, 2, 15)
            }
        };

        /// <summary>  
        /// Retrieves all reviews.  
        /// </summary>  
        [HttpGet]
        public IActionResult GetAll() => Ok(_reviews);

        /// <summary>  
        /// Retrieves a specific review by ID. 
        /// </summary>  
        /// <param name="gameId">The review's unique ID.</param>  
        [HttpGet("game/{gameId}")]
        public IActionResult GetByGameId(int gameId)
        {
            var reviews = _reviews.Where(r => r.GameId == gameId).ToList();
            return Ok(reviews);
        }

        /// <summary>  
        /// Creates a new review.  
        /// </summary>  
        /// <param name="review">The review data in JSON format.</param>  
        [HttpPost]
        public IActionResult Create([FromBody] Review review)
        { 
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            review.Id = _reviews.Count + 1;
            review.ReviewDate = DateTime.Now;
            _reviews.Add(review);
            return CreatedAtAction(nameof(GetByGameId), new { id = review.Id }, review);
        }

        /// <summary>  
        /// Updates an existing review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        /// <param name="updatedReview">Updated review data in JSON format.</param>  
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Review updatedReview)
        {
            var review = _reviews.FirstOrDefault(r => r.Id == id);
            if (review == null) return NotFound();

            review.Comment = updatedReview.Comment;
            review.Rating = updatedReview.Rating;
            return NoContent();
        }

        /// <summary>  
        /// Deletes a review by ID. 
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _reviews.FirstOrDefault(r => r.Id == id);
            if (review == null) return NotFound();

            _reviews.Remove(review);
            return NoContent();
        }
    }
}
