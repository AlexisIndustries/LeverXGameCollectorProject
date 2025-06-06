using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        /// <summary>  
        /// Retrieves all developers.  
        /// </summary>  
        [HttpGet]
        [ProducesResponseType<IEnumerable<DeveloperResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _developerService.GetAllDevelopersAsync());

        /// <summary>  
        /// Retrieves a specific developer by ID.
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        [HttpGet("{id}")]
        [ProducesResponseType<DeveloperResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            return developer == null ? NotFound() : Ok(developer);
        }

        /// <summary>  
        /// Creates a new developer.  
        /// </summary>  
        /// <param name="developer">The developer data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeveloperRequestModel developer)
        {
            await _developerService.CreateDeveloperAsync(developer);
            return Created();
        }

        /// <summary>  
        /// Updates an existing developer by ID. 
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        /// <param name="updatedDeveloper">Updated developer data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDeveloperRequestModel updatedDeveloper)
        {
            await _developerService.UpdateDeveloperAsync(id, updatedDeveloper);

            return NoContent();
        }

        /// <summary>  
        /// Deletes a developer by ID. 
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var developer = await _developerService.GetDeveloperByIdAsync(id);
            if (developer == null) return NotFound();

            await _developerService.DeleteDeveloperAsync(id);
            return NoContent();
        }
    }
}
