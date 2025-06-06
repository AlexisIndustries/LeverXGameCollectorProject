using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>  
        /// Retrieves all games.  
        /// </summary> 
        [HttpGet]
        [ProducesResponseType<IEnumerable<GameResponseModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _gameService.GetAllGamesAsync());

        /// <summary>  
        /// Retrieves a specific game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param> 
        [HttpGet("{id}")]
        [ProducesResponseType<GameResponseModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            return game == null ? NotFound() : Ok(game);
        }

        /// <summary>  
        /// Creates a new game.  
        /// </summary>  
        /// <param name="game">The game data in JSON format.</param> 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameRequestModel game)
        {
            await _gameService.CreateGameAsync(game);
            return Created();
        }

        /// <summary>  
        /// Updates an existing game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param>  
        /// <param name="updatedGame">Updated game data in JSON format.</param> 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameRequestModel updatedGame)
        {
            await _gameService.UpdateGameAsync(id, updatedGame);

            return NoContent();
        }

        /// <summary>  
        /// Deletes a game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound();

            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }
    }
}
