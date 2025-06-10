using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryDeveloperRepository : IDeveloperRepository
    {
        private static List<DeveloperEntity> _developers = new()
        {
            new DeveloperEntity
            {
                Id = 1,
                Name = "CD Projekt Red",
                Country = "Poland",
                Website = "https://cdprojektred.com",
                Founded = new DateTime(1994, 5, 1)
            },
            new DeveloperEntity
            {
                Id = 2,
                Name = "Nintendo",
                Country = "Japan",
                Website = "https://nintendo.com",
                Founded = new DateTime(1889, 9, 23)
            }
        };

        public Task<DeveloperEntity> GetByIdAsync(int id)
            => Task.FromResult(_developers.FirstOrDefault(d => d.Id == id));

        public Task<IEnumerable<DeveloperEntity>> GetAllAsync()
            => Task.FromResult(_developers.AsEnumerable());

        public Task<int> AddAsync(DeveloperEntity developer)
        {
            developer.Id = _developers.Max(d => d.Id) + 1;
            _developers.Add(developer);
            return Task.FromResult(developer.Id);
        }

        public Task UpdateAsync(DeveloperEntity entity)
        {
            var index = _developers.FindIndex(d => d.Id == entity.Id);
            if (index >= 0) _developers[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _developers.RemoveAll(d => d.Id == id);
            return Task.CompletedTask;
        }
    }
}