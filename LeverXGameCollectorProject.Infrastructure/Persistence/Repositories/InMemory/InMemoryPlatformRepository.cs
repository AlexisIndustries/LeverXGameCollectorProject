using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryPlatformRepository : IPlatformRepository
    {
        private static List<Platform> _platforms = new()
        {
            new Platform
            {
                Id = 1,
                Name = "PC",
                Manufacturer = "Various",
                ReleaseYear = 1970
            },
            new Platform
            {
                Id = 2,
                Name = "PlayStation 5",
                Manufacturer = "Sony",
                ReleaseYear = 2020
            }
        };

        public Task<Platform> GetByIdAsync(int id)
           => Task.FromResult(_platforms.FirstOrDefault(p => p.Id == id));

        public Task<IEnumerable<Platform>> GetAllAsync()
            => Task.FromResult(_platforms.AsEnumerable());

        public Task AddAsync(Platform platform)
        {
            platform.Id = _platforms.Max(p => p.Id) + 1;
            _platforms.Add(platform);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Platform entity)
        {
            var index = _platforms.FindIndex(p => p.Id == entity.Id);
            if (index >= 0) _platforms[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _platforms.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }
    }
}
