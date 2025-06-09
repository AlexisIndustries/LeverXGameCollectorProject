using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;

namespace LeverXGameCollectorProject.Application.Repositories.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewEntity>
    {
        Task<IEnumerable<ReviewEntity>> GetByGameAsync(int gameId);
    }
}
