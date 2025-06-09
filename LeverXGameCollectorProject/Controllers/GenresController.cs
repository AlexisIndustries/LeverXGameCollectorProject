using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        //private readonly IGenreService _genreService;
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //  public GenresController(IGenreService genreService)
        //  {
        //      _genreService = genreService;
        //  }

        /// <summary>  
        /// Retrieves all genres.  
        /// </summary>  
        [HttpGet]
        [ProducesResponseType<IEnumerable<GenreResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllGenresQuery()));

        /// <summary>  
        /// Retrieves a specific genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpGet("{id}")]
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
        public async Task<IActionResult> Create([FromBody] CreateGenreCommand genre)
        {
            await _mediator.Send(genre);
            return Created();
        }

        /// <summary>  
        /// Updates an existing genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        /// <param name="updatedGenre">Updated genre data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreCommand updatedGenre)
        {
            updatedGenre.Id = id;
            await _mediator.Send(updatedGenre);
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
