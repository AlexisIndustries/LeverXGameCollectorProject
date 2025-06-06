using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        //private readonly IReviewService _reviewService;
        private IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // public ReviewsController(IReviewService reviewService)
        // {
        //     _reviewService = reviewService;
        // }

        /// <summary>  
        /// Retrieves all reviews.  
        /// </summary>  
        [HttpGet]
        [ProducesResponseType<IEnumerable<ReviewResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllReviewQuery()));

        /// <summary>  
        /// Retrieves a specific review by ID. 
        /// </summary>  
        /// <param name="gameId">The review's unique ID.</param>  
        [HttpGet("game/{gameId}")]
        [ProducesResponseType<ReviewResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByGameId(int gameId)
        {
            var reviews = await _mediator.Send(new GetReviewByGameIdQuery(gameId));
            return Ok(reviews);
        }

        /// <summary>  
        /// Retrieves a specific review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reviews = await _mediator.Send(new GetReviewByIdQuery(id));
            return Ok(reviews);
        }

        /// <summary>  
        /// Creates a new review.  
        /// </summary>  
        /// <param name="review">The review data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand review)
        { 
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            await _mediator.Send(review);
            return Created();
        }

        /// <summary>  
        /// Updates an existing review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        /// <param name="updatedReview">Updated review data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewCommand updatedReview)
        {
            updatedReview.Id = id;
            await _mediator.Send(updatedReview);

            return NoContent();
        }

        /// <summary>  
        /// Deletes a review by ID. 
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = _mediator.Send(new GetReviewByIdQuery(id));
            if (review == null) return NotFound();

            await _mediator.Send(new DeleteReviewCommand(id));
            return NoContent();
        }
    }
}
