using LeverXGameCollectorProject.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseModel>> GetAllReviewsAsync();
        Task<ReviewResponseModel> GetReviewByIdAsync(int id);
        Task CreateReviewAsync(CreateReviewRequestModel reviewDto);
        Task UpdateReviewAsync(int id, UpdateReviewRequestModel reviewDto);
        Task DeleteReviewAsync(int id);
        Task<IEnumerable<ReviewResponseModel>> GetReviewsByGameAsync(int gameId);
    }
}
