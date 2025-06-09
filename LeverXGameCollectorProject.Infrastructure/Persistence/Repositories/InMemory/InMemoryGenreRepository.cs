using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryGenreRepository : IGenreRepository
    {
        private static List<Genre> _genres = new()
        {
            new Genre
            {
                Id = 1,
                Name = "RPG",
                Description = "Role-playing games",
                Popularity = "High"
            },
            new Genre
            {
                Id = 2,
                Name = "Action",
                Description = "Fast-paced games",
                Popularity = "Very High"
            }
        };
        public Task<Genre> GetByIdAsync(int id)
            => Task.FromResult(_genres.FirstOrDefault(g => g.Id == id));

        public Task<IEnumerable<Genre>> GetAllAsync()
            => Task.FromResult(_genres.AsEnumerable());

        public Task AddAsync(Genre genre)
        {
            genre.Id = _genres.Max(g => g.Id) + 1;
            _genres.Add(genre);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Genre entity)
        {
            var index = _genres.FindIndex(g => g.Id == entity.Id);
            if (index >= 0) _genres[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _genres.RemoveAll(g => g.Id == id);
            return Task.CompletedTask;
        }
    }
}
