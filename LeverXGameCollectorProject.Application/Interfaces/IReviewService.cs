using LeverXGameCollectorProject.Application.DTOs.Review;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseModel>> GetAllReviewsAsync();
        Task<ReviewResponseModel> GetReviewByIdAsync(int id);
        Task<int> CreateReviewAsync(CreateReviewRequestModel reviewDto);
        Task UpdateReviewAsync(int id, UpdateReviewRequestModel reviewDto);
        Task DeleteReviewAsync(int id);
        Task<IEnumerable<ReviewResponseModel>> GetReviewsByGameAsync(int gameId);
    }
}
