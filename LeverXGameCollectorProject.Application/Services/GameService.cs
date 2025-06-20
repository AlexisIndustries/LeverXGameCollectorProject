﻿using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
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

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Game>>(games);
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return _mapper.Map<Game>(game);
        }

        public async Task<int> CreateGameAsync(CreateGameRequestModel gameDto)
        {
            var game = _mapper.Map<GameEntity>(gameDto);
            var id = await _gameRepository.AddAsync(game);
            return id;
        }

        public async Task UpdateGameAsync(int id, UpdateGameRequestModel gameDto)
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
