using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryPlatformRepository : IPlatformRepository
    {
        private static List<PlatformEntity> _platforms = new()
        {
            new PlatformEntity
            {
                Id = 1,
                Name = "PC",
                Manufacturer = "Various",
                ReleaseYear = 1970
            },
            new PlatformEntity
            {
                Id = 2,
                Name = "PlayStation 5",
                Manufacturer = "Sony",
                ReleaseYear = 2020
            }
        };

        public async Task<PlatformEntity> GetByIdAsync(int id)
           => _platforms.FirstOrDefault(p => p.Id == id);

        public async Task<IEnumerable<PlatformEntity>> GetAllAsync()
            => _platforms.AsEnumerable();

        public async Task<int> AddAsync(PlatformEntity platform)
        {
            platform.Id = _platforms.Max(p => p.Id) + 1;
            _platforms.Add(platform);
            return platform.Id;
        }

        public async Task UpdateAsync(PlatformEntity entity)
        {
            var index = _platforms.FindIndex(p => p.Id == entity.Id);
            if (index >= 0) _platforms[index] = entity;
        }

        public async Task DeleteAsync(int id)
        {
            _platforms.RemoveAll(p => p.Id == id);
        }
    }
}
