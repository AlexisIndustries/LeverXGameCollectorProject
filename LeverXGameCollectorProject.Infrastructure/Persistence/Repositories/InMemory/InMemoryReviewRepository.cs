using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryReviewRepository : IReviewRepository
    {
        private static List<ReviewEntity> _reviews = new()
    {
        new ReviewEntity { Id = 1, Game = new GameEntity { Id = 1 }, Rating = 5 },
        new ReviewEntity { Id = 2, Game = new GameEntity { Id = 1 }, Rating = 4 }
    };

        public async Task<ReviewEntity> GetByIdAsync(int id)
            => _reviews.FirstOrDefault(r => r.Id == id);

        public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
            => _reviews.AsEnumerable();

        public async Task<int> AddAsync(ReviewEntity review)
        {
            review.Id = _reviews.Max(r => r.Id) + 1;
            _reviews.Add(review);
            return review.Id;
        }

        public async Task UpdateAsync(ReviewEntity entity)
        {
            var index = _reviews.FindIndex(r => r.Id == entity.Id);
            if (index >= 0) _reviews[index] = entity;
        }

        public async Task DeleteAsync(int id)
        {
            _reviews.RemoveAll(r => r.Id == id);
        }

        public Task<IEnumerable<ReviewEntity>> GetByGameAsync(int gameId)
            => Task.FromResult(_reviews.Where(r => r.Game.Id == gameId));
    }
}
