using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Features.Developer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DevelopersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>  
        /// Retrieves all developers.  
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType<IEnumerable<DeveloperResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var developers = await _mediator.Send(new GetAllDevelopersQuery());
            return Ok(developers.Select(_mapper.Map<DeveloperResponseModel>));
        }
        /// <summary>  
        /// Retrieves a specific developer by ID.
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param> 
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType<DeveloperResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var developer = await _mediator.Send(new GetDeveloperByIdQuery(id));
            var model = _mapper.Map<DeveloperResponseModel>(developer);
            return model == null ? NotFound() : Ok(model);
        }

        /// <summary>  
        /// Creates a new developer.  
        /// </summary>  
        /// <param name="developer">The developer data in JSON format.</param>  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeveloperRequestModel developer)
        {
            var id = await _mediator.Send(new CreateDeveloperCommand(developer));
            Dictionary<string, int> res = new()
            {
                { "id", id }
            };
            return StatusCode(StatusCodes.Status201Created, res);
        }

        /// <summary>  
        /// Updates an existing developer by ID. 
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        /// <param name="updatedDeveloper">Updated developer data in JSON format.</param>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDeveloperRequestModel updatedDeveloper)
        {
            await _mediator.Send(new UpdateDeveloperCommand(id, updatedDeveloper));
            return NoContent();
        }

        /// <summary>  
        /// Deletes a developer by ID. 
        /// </summary>  
        /// <param name="id">The developer's unique ID.</param>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var developer = await _mediator.Send(new GetDeveloperByIdQuery(id));
            if (developer == null) return NotFound();

            await _mediator.Send(new DeleteDeveloperCommand(id));
            return NoContent();
        }
    }
}
