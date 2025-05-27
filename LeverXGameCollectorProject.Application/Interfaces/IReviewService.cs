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
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task CreateReviewAsync(CreateReviewDto reviewDto);
        Task UpdateReviewAsync(int id, UpdateReviewDto reviewDto);
        Task DeleteReviewAsync(int id);
    }
}
