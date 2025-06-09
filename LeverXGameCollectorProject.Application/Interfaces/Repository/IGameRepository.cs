using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Application.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<GameEntity>
    {
        Task<IEnumerable<GameEntity>> GetByPlatformAsync(int platformId);
    }
}
