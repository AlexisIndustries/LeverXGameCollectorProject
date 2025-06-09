using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        //private readonly IPlatformService _plaformService;
        private readonly IMediator _mediator;

        public PlatformsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // public PlatformsController(IPlatformService plaformService)
        // {
        //     _plaformService = plaformService;
        // }

        /// <summary>  
        /// Retrieves all platforms.  
        /// </summary>  
        [HttpGet]
        [ProducesResponseType<IEnumerable<PlatformResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllPlatformsQuery()));

        /// <summary>  
        /// Retrieves a specific platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpGet("{id}")]
        [ProducesResponseType<PlatformResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var platform = await _mediator.Send(new GetPlatformByIdQuery(id));
            return platform == null ? NotFound() : Ok(platform);
        }

        /// <summary>  
        /// Creates a new platform.  
        /// </summary>  
        /// <param name="platform">The platform data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlatformCommand platform)
        {
            var id = await _mediator.Send(platform);
            Dictionary<string, int> res = new()
            {
                { "id", id }
            };
            return StatusCode(StatusCodes.Status201Created, res);
        }

        /// <summary>  
        /// Updates an existing platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        /// <param name="updatedPlatform">Updated platform data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlatformCommand updatedPlatform)
        {
            updatedPlatform.Id = id;
            await _mediator.Send(updatedPlatform);
            return NoContent();
        }

        /// <summary>  
        /// Deletes a platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var platform = _mediator.Send(new GetPlatformByIdQuery(id));
            if (platform == null) return NotFound();

            await _mediator.Send(new DeletePlatformCommand(id));
            return NoContent();
        }
    }
}
