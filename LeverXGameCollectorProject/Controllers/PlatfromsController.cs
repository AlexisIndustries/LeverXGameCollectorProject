using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private static List<Platform> _platforms = new()
        {
            new Platform
            {
                Id = 1,
                Name = "PC",
                Manufacturer = "Various",
                ReleaseYear = 1970
            },
            new Platform
            {
                Id = 2,
                Name = "PlayStation 5",
                Manufacturer = "Sony",
                ReleaseYear = 2020
            }
        };

        /// <summary>  
        /// Retrieves all platforms.  
        /// </summary>  
        [HttpGet]
        public IActionResult GetAll() => Ok(_platforms);

        /// <summary>  
        /// Retrieves a specific platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var platform = _platforms.FirstOrDefault(p => p.Id == id);
            return platform == null ? NotFound() : Ok(platform);
        }

        /// <summary>  
        /// Creates a new platform.  
        /// </summary>  
        /// <param name="platform">The platform data in JSON format.</param>  
        [HttpPost]
        public IActionResult Create([FromBody] Platform platform)
        {
            platform.Id = _platforms.Count + 1;
            _platforms.Add(platform);
            return CreatedAtAction(nameof(GetById), new { id = platform.Id }, platform);
        }

        /// <summary>  
        /// Updates an existing platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        /// <param name="updatedPlatform">Updated platform data in JSON format.</param>  
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Platform updatedPlatform)
        {
            var platform = _platforms.FirstOrDefault(p => p.Id == id);
            if (platform == null) return NotFound();

            platform.Name = updatedPlatform.Name;
            platform.Manufacturer = updatedPlatform.Manufacturer;
            platform.ReleaseYear = updatedPlatform.ReleaseYear;
            return NoContent();
        }

        /// <summary>  
        /// Deletes a platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var platform = _platforms.FirstOrDefault(p => p.Id == id);
            if (platform == null) return NotFound();

            _platforms.Remove(platform);
            return NoContent();
        }
    }
}
