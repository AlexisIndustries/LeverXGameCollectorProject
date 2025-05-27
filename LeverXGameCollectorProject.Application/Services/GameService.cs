using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }

        public async Task<GameDto> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return _mapper.Map<GameDto>(game);
        }

        public async Task CreateGameAsync(CreateGameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            await _gameRepository.AddAsync(game);
        }

        public async Task UpdateGameAsync(int id, UpdateGameDto gameDto)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            _mapper.Map(gameDto, game);
            await _gameRepository.UpdateAsync(game);
        }

        public async Task DeleteGameAsync(int id)
        {
            await _gameRepository.DeleteAsync(id);
        }
    }
}
