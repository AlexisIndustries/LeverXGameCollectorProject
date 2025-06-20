﻿using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public ReviewsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>  
        /// Retrieves all reviews.  
        /// </summary>  
        [HttpGet]
        [ProducesResponseType<IEnumerable<ReviewResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _mediator.Send(new GetAllReviewQuery());
            return Ok(reviews.Select(_mapper.Map<ReviewResponseModel>));
        }

        /// <summary>  
        /// Retrieves a specific review by ID. 
        /// </summary>  
        /// <param name="gameId">The review's unique ID.</param>  
        [HttpGet("game/{gameId}")]
        [ProducesResponseType<ReviewResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByGameId(int gameId)
        {
            var review = await _mediator.Send(new GetReviewByGameIdQuery(gameId));
            return Ok(_mapper.Map<ReviewResponseModel>(review));
        }

        /// <summary>  
        /// Retrieves a specific review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reviews = await _mediator.Send(new GetReviewByIdQuery(id));
            return Ok(_mapper.Map<ReviewResponseModel>(reviews));
        }

        /// <summary>  
        /// Creates a new review.  
        /// </summary>  
        /// <param name="review">The review data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequestModel review)
        { 
            if (review.Rating < 1 || review.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            var id = await _mediator.Send(new CreateReviewCommand(review));
            Dictionary<string, int> res = new()
            {
                { "id", id }
            };
            return StatusCode(StatusCodes.Status201Created, res);
        }

        /// <summary>  
        /// Updates an existing review by ID.  
        /// </summary>  
        /// <param name="id">The review's unique ID.</param>  
        /// <param name="updatedReview">Updated review data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewRequestModel updatedReview)
        {
            await _mediator.Send(new UpdateReviewCommand(id, updatedReview));

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
