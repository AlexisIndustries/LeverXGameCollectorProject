using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Review>> GetReviewsByGameAsync(int gameId)
        {
            var reviews = await _reviewRepository.GetByGameAsync(gameId);
            return _mapper.Map<IEnumerable<Review>>(reviews);
        }

        public async Task<int> CreateReviewAsync(CreateReviewRequestModel reviewDto)
        {
            var review = _mapper.Map<ReviewEntity>(reviewDto);
            var id = await _reviewRepository.AddAsync(review);
            return id;
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Review>>(reviews);
        
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            return _mapper.Map<Review>(review);
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewRequestModel reviewDto)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            _mapper.Map(reviewDto, review);
            await _reviewRepository.UpdateAsync(review);
        }
    }
}
