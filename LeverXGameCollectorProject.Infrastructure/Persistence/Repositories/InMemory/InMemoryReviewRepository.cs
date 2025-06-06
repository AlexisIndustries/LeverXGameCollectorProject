using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryReviewRepository : IReviewRepository
    {
        private static List<Review> _reviews = new()
    {
        new Review { Id = 1, Game = new Game{ Id = 1 }, Rating = 5 },
        new Review { Id = 2, Game = new Game{ Id = 1 }, Rating = 4 }
    };

        public Task<Review> GetByIdAsync(int id)
            => Task.FromResult(_reviews.FirstOrDefault(r => r.Id == id));

        public Task<IEnumerable<Review>> GetAllAsync()
            => Task.FromResult(_reviews.AsEnumerable());

        public Task AddAsync(Review review)
        {
            review.Id = _reviews.Max(r => r.Id) + 1;
            _reviews.Add(review);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Review entity)
        {
            var index = _reviews.FindIndex(r => r.Id == entity.Id);
            if (index >= 0) _reviews[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _reviews.RemoveAll(r => r.Id == id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Review>> GetByGameAsync(int gameId)
            => Task.FromResult(_reviews.Where(r => r.Game.Id == gameId));
    }
}
