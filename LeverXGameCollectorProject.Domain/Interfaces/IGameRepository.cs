using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Domain.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetByPlatformAsync(int platformId);
    }
}
