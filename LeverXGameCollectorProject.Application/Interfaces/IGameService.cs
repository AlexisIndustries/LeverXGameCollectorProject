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
        Task<IEnumerable<GameResponseModel>> GetAllGamesAsync();
        Task<GameResponseModel> GetGameByIdAsync(int id);
        Task CreateGameAsync(CreateGameRequestModel gameDto);
        Task UpdateGameAsync(int id, UpdateGameRequestModel gameDto);
        Task DeleteGameAsync(int id);
    }
}
