using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {

        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>  
        /// Retrieves all genres.  
        /// </summary>  
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _genreService.GetAllGenresAsync());

        /// <summary>  
        /// Retrieves a specific genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            return genre == null ? NotFound() : Ok(genre);
        }

        /// <summary>  
        /// Creates a new genre.  
        /// </summary>  
        /// <param name="genre">The genre data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreDto genre)
        {
            await _genreService.CreateGenreAsync(genre);
            return Created();
        }

        /// <summary>  
        /// Updates an existing genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        /// <param name="updatedGenre">Updated genre data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreDto updatedGenre)
        {
            await _genreService.UpdateGenreAsync(id, updatedGenre);

            return NoContent();
        }

        /// <summary>  
        /// Deletes a genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();

            await _genreService.DeleteGenreAsync(id);
            return NoContent();
        }
    }
}
