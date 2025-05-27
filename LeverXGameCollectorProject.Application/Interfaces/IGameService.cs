using LeverXGameCollectorProject.Application.DTOs.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<GameDto> GetGameByIdAsync(int id);
        Task CreateGameAsync(CreateGameDto gameDto);
        Task UpdateGameAsync(int id, UpdateGameDto gameDto);
        Task DeleteGameAsync(int id);
    }
}
