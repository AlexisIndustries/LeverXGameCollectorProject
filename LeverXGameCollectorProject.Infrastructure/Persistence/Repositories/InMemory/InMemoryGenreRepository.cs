using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryGenreRepository : IGenreRepository
    {
        private static List<GenreEntity> _genres = new()
        {
            new GenreEntity
            {
                Id = 1,
                Name = "RPG",
                Description = "Role-playing games",
                Popularity = "High"
            },
            new GenreEntity
            {
                Id = 2,
                Name = "Action",
                Description = "Fast-paced games",
                Popularity = "Very High"
            }
        };
        public async Task<GenreEntity> GetByIdAsync(int id)
            => _genres.FirstOrDefault(g => g.Id == id);

        public async Task<IEnumerable<GenreEntity>> GetAllAsync()
            => _genres.AsEnumerable();

        public async Task<int> AddAsync(GenreEntity genre)
        {
            genre.Id = _genres.Max(g => g.Id) + 1;
            _genres.Add(genre);
            return genre.Id;
        }

        public async Task UpdateAsync(GenreEntity entity)
        {
            var index = _genres.FindIndex(g => g.Id == entity.Id);
            if (index >= 0) _genres[index] = entity;
        }

        public async Task DeleteAsync(int id)
        {
            _genres.RemoveAll(g => g.Id == id);
        }
    }
}
