using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Features.Platform.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PlatformsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>  
        /// Retrieves all platforms.  
        /// </summary>  
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType<IEnumerable<PlatformResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var platforms = await _mediator.Send(new GetAllPlatformsQuery());
            return Ok(platforms.Select(_mapper.Map<PlatformResponseModel>));
        }

        /// <summary>  
        /// Retrieves a specific platform by ID.  
        /// </summary>  
        /// <param name="id">The platform's unique ID.</param>  
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType<PlatformResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var platform = await _mediator.Send(new GetPlatformByIdQuery(id));
            var model = _mapper.Map<PlatformResponseModel>(platform);
            return platform == null ? NotFound() : Ok(platform);
        }

        /// <summary>  
        /// Creates a new platform.  
        /// </summary>  
        /// <param name="platform">The platform data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlatformRequestModel platform)
        {
            var id = await _mediator.Send(new CreatePlatformCommand(platform));
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlatformRequestModel updatedPlatform)
        {
            await _mediator.Send(new UpdatePlatformCommand(id, updatedPlatform));
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
