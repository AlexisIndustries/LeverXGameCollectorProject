using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _plaformService;

        public PlatformsController(IPlatformService plaformService)
        {
            _plaformService = plaformService;
        }

        /// <summary>  
        /// Retrieves all platforms.  
        /// </summary>  
        [HttpGet]
        [ProducesResponseType<IEnumerable<PlatformResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _plaformService.GetAllPlatformsAsync());

        /// <summary>  
        /// Retrieves a specific platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpGet("{id}")]
        [ProducesResponseType<PlatformResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var platform = await _plaformService.GetPlatformByIdAsync(id);
            return platform == null ? NotFound() : Ok(platform);
        }

        /// <summary>  
        /// Creates a new platform.  
        /// </summary>  
        /// <param name="platform">The platform data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlatformRequestModel platform)
        {
            await _plaformService.CreatePlatformAsync(platform);
            return Created();
        }

        /// <summary>  
        /// Updates an existing platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        /// <param name="updatedPlatform">Updated platform data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlatformRequestModel updatedPlatform)
        {
            await _plaformService.UpdatePlatformAsync(id, updatedPlatform);

            return NoContent();
        }

        /// <summary>  
        /// Deletes a platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var platform = _plaformService.GetPlatformByIdAsync(id);
            if (platform == null) return NotFound();

            await _plaformService.DeletePlatformAsync(id);
            return NoContent();
        }
    }
}
