using LeverXGameCollectorProject.Application.DTOs.Game;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameResponseModel>> GetAllGamesAsync();
        Task<GameResponseModel> GetGameByIdAsync(int id);
        Task<int> CreateGameAsync(CreateGameRequestModel gameDto);
        Task UpdateGameAsync(int id, UpdateGameRequestModel gameDto);
        Task DeleteGameAsync(int id);
    }
}
