using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByGameAsync(int gameId);
    }
}
