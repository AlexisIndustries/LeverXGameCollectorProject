using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private static List<Genre> _genres = new() 
        {
            new Genre
            {
                Id = 1,
                Name = "RPG",
                Description = "Role-playing games",
                Popularity = "High"
            },
            new Genre
            {
                Id = 2,
                Name = "Action",
                Description = "Fast-paced games",
                Popularity = "Very High"
            }
        };

        /// <summary>  
        /// Retrieves all genres.  
        /// </summary>  
        [HttpGet]
        public IActionResult GetAll() => Ok(_genres);

        /// <summary>  
        /// Retrieves a specific genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            return genre == null ? NotFound() : Ok(genre);
        }

        /// <summary>  
        /// Creates a new genre.  
        /// </summary>  
        /// <param name="genre">The genre data in JSON format.</param>  
        [HttpPost]
        public IActionResult Create([FromBody] Genre genre)
        {
            genre.Id = _genres.Count + 1;
            _genres.Add(genre);
            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
        }

        /// <summary>  
        /// Updates an existing genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        /// <param name="updatedGenre">Updated genre data in JSON format.</param>  
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Genre updatedGenre)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return NotFound();

            genre.Name = updatedGenre.Name;
            genre.Description = updatedGenre.Description;
            genre.Popularity = updatedGenre.Popularity;
            return NoContent();
        }

        /// <summary>  
        /// Deletes a genre by ID.  
        /// </summary>  
        /// <param name="id">The genre's unique ID.</param>  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return NotFound();

            _genres.Remove(genre);
            return NoContent();
        }
    }
}
