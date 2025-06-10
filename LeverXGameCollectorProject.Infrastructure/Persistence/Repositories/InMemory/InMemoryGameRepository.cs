using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryGameRepository : IGameRepository
    {
        private static List<GameEntity> _games = new()
        {
            new GameEntity { Id = 1, Title = "The Witcher 3", Platform = new PlatformEntity { Id = 1 } },
            new GameEntity { Id = 2, Title = "Super Mario Odyssey", Platform = new PlatformEntity { Id = 2 } }
        };

        public async Task<GameEntity> GetByIdAsync(int id)
            => _games.FirstOrDefault(g => g.Id == id);

        public Task<IEnumerable<GameEntity>> GetAllAsync()
            => Task.FromResult(_games.AsEnumerable());

        public async Task<int> AddAsync(GameEntity game)
        {
            game.Id = _games.Max(g => g.Id) + 1;
            _games.Add(game);
            return game.Id;
        }

        public async Task UpdateAsync(GameEntity entity)
        {
            var index = _games.FindIndex(g => g.Id == entity.Id);
            if (index >= 0) _games[index] = entity;
        }

        public async Task DeleteAsync(int id)
        {
            _games.RemoveAll(g => g.Id == id);
        }

        public async Task<IEnumerable<GameEntity>> GetByPlatformAsync(int platformId)
            => _games.Where(g => g.Platform.Id == platformId);
    }
}
