using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.InMemory
{
    public class InMemoryGameRepository : IGameRepository
    {
        private static List<Game> _games = new()
        {
            new Game { Id = 1, Title = "The Witcher 3", PlatformId = [1] },
            new Game { Id = 2, Title = "Super Mario Odyssey", PlatformId = [2] }
        };

        public Task<Game> GetByIdAsync(int id)
            => Task.FromResult(_games.FirstOrDefault(g => g.Id == id));

        public Task<IEnumerable<Game>> GetAllAsync()
            => Task.FromResult(_games.AsEnumerable());

        public Task AddAsync(Game game)
        {
            game.Id = _games.Max(g => g.Id) + 1;
            _games.Add(game);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Game entity)
        {
            var index = _games.FindIndex(g => g.Id == entity.Id);
            if (index >= 0) _games[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _games.RemoveAll(g => g.Id == id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Game>> GetByPlatformAsync(int platformId)
            => Task.FromResult(_games.Where(g => g.PlatformId.Contains(platformId)));
    }
}
