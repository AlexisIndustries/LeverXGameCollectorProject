﻿using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenresController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>  
        /// Retrieves all genres.  
        /// </summary>  
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType<IEnumerable<GenreResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _mediator.Send(new GetAllGenresQuery());
            return Ok(genres.Select(_mapper.Map<GenreResponseModel>));
        }
        /// <summary>  
        /// Retrieves a specific genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType<GenreResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _mediator.Send(new GetGenreByIdQuery(id));
            return genre == null ? NotFound() : Ok(genre);
        }

        /// <summary>  
        /// Creates a new genre.  
        /// </summary>  
        /// <param name="genre">The genre data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreRequestModel genre)
        {
            var id = await _mediator.Send(new CreateGenreCommand(genre));
            Dictionary<string, int> res = new()
            {
                { "id", id }
            };
            return StatusCode(StatusCodes.Status201Created, res);
        }

        /// <summary>  
        /// Updates an existing genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        /// <param name="updatedGenre">Updated genre data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreRequestModel updatedGenre)
        {
            await _mediator.Send(new UpdateGenreCommand(id, updatedGenre));
            return NoContent();
        }

        /// <summary>  
        /// Deletes a genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = _mediator.Send(new GetGenreByIdQuery(id));
            if (genre == null) return NotFound();

            await _mediator.Send(new DeleteGenreCommand(id));
            return NoContent();
        }
    }
}
