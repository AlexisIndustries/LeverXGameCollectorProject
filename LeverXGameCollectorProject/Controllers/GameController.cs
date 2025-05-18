using LeverXGameCollectorProject.Models;
using Microsoft.AspNetCore.Mvc;


namespace LeverXGameCollectorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private static List<Game> _games = new()
        {
            new Game
            {
                Id = 1,
                Title = "The Witcher 3",
                ReleaseDate = new DateTime(2015, 5, 19),
                Description = "Action RPG game",
                PlatformId = 1,
                GenreId = 1
            },
            new Game
            {
                Id = 2,
                Title = "Super Mario Odyssey",
                ReleaseDate = new DateTime(2017, 10, 27),
                Description = "Platformer game",
                PlatformId = 2,
                GenreId = 2
            }
        };

        /// <summary>  
        /// Retrieves all games.  
        /// </summary> 
        [HttpGet]
        public IActionResult GetAll() => Ok(_games);

        /// <summary>  
        /// Retrieves a specific game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param> 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            return game == null ? NotFound() : Ok(game);
        }

        /// <summary>  
        /// Creates a new game.  
        /// </summary>  
        /// <param name="game">The game data in JSON format.</param> 
        [HttpPost]
        public IActionResult Create([FromBody] Game game)
        {
            game.Id = _games.Count + 1;
            _games.Add(game);
            return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
        }

        /// <summary>  
        /// Updates an existing game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param>  
        /// <param name="updatedGame">Updated game data in JSON format.</param> 
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Game updatedGame)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game == null) return NotFound();

            game.Title = updatedGame.Title;
            game.Description = updatedGame.Description;
            game.PlatformId = updatedGame.PlatformId;
            game.ReleaseDate = updatedGame.ReleaseDate;
            game.GenreId = updatedGame.GenreId;
            return NoContent();
        }

        /// <summary>  
        /// Deletes a game by ID.  
        /// </summary>  
        /// <param name="id">The game's unique ID.</param> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game == null) return NotFound();

            _games.Remove(game);
            return NoContent();
        }
    }
}
