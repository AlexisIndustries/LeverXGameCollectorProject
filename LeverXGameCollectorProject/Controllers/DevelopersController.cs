using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private static List<Developer> _developers = new() 
        {
            new Developer
            {
                Id = 1,
                Name = "CD Projekt Red",
                Country = "Poland",
                Website = "https://cdprojektred.com",
                Founded = new DateTime(1994, 5, 1)
            },
            new Developer
            {
                Id = 2,
                Name = "Nintendo",
                Country = "Japan",
                Website = "https://nintendo.com",
                Founded = new DateTime(1889, 9, 23)
            }
        };

        /// <summary>  
        /// Retrieves all developers.  
        /// </summary>  
        [HttpGet]
        public IActionResult GetAll() => Ok(_developers);

        /// <summary>  
        /// Retrieves a specific developer by ID.
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var developer = _developers.FirstOrDefault(d => d.Id == id);
            return developer == null ? NotFound() : Ok(developer);
        }

        /// <summary>  
        /// Creates a new developer.  
        /// </summary>  
        /// <param name="developer">The developer data in JSON format.</param>  
        [HttpPost]
        public IActionResult Create([FromBody] Developer developer)
        {
            developer.Id = _developers.Count + 1;
            _developers.Add(developer);
            return CreatedAtAction(nameof(GetById), new { id = developer.Id }, developer);
        }

        /// <summary>  
        /// Updates an existing developer by ID. 
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        /// <param name="updatedDeveloper">Updated developer data in JSON format.</param>  
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Developer updatedDeveloper)
        {
            var developer = _developers.FirstOrDefault(d => d.Id == id);
            if (developer == null) return NotFound();

            developer.Founded = updatedDeveloper.Founded;
            developer.Name = updatedDeveloper.Name;
            developer.Country = updatedDeveloper.Country;
            developer.Website = updatedDeveloper.Website;
            return NoContent();
        }

        /// <summary>  
        /// Deletes a developer by ID. 
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var developer = _developers.FirstOrDefault(d => d.Id == id);
            if (developer == null) return NotFound();

            _developers.Remove(developer);
            return NoContent();
        }
    }
}
