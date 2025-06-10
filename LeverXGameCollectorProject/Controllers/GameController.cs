using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Application.Features.Game.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GamesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>  
        /// Retrieves all games.  
        /// </summary> 
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType<IEnumerable<GameResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var games = await _mediator.Send(new GetAllGamesQuery());
            return Ok(games.Select(_mapper.Map<GameResponseModel>));
        }
        /// <summary>  
        /// Retrieves a specific game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param> 
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType<GameResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _mediator.Send(new GetGameByIdQuery(id));
            var model = _mapper.Map<GameResponseModel>(game);
            return model == null ? NotFound() : Ok(model);
        }

        /// <summary>  
        /// Creates a new game.  
        /// </summary>  
        /// <param name="game">The game data in JSON format.</param> 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameRequestModel game)
        {
            var id = await _mediator.Send(new CreateGameCommand(game));
            Dictionary<string, int> res = new()
            {
                { "id", id }
            };
            return StatusCode(StatusCodes.Status201Created, res);
        }

        /// <summary>  
        /// Updates an existing game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param>  
        /// <param name="updatedGame">Updated game data in JSON format.</param> 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameRequestModel updatedGame)
        {
            await _mediator.Send(new UpdateGameCommand(id, updatedGame));
            return NoContent();
        }

        /// <summary>  
        /// Deletes a game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = _mediator.Send(new GetGameByIdQuery(id));
            if (game == null) return NotFound();

            await _mediator.Send(new DeleteGameCommand(id));
            return NoContent();
        }
    }
}
