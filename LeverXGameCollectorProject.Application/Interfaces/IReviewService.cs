using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<int> CreateReviewAsync(CreateReviewRequestModel reviewDto);
        Task UpdateReviewAsync(int id, UpdateReviewRequestModel reviewDto);
        Task DeleteReviewAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByGameAsync(int gameId);
    }
}
