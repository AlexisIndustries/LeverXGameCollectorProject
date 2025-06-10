using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task<int> CreateGameAsync(CreateGameRequestModel gameDto);
        Task UpdateGameAsync(int id, UpdateGameRequestModel gameDto);
        Task DeleteGameAsync(int id);
    }
}
