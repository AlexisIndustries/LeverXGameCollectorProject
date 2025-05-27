using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<ReviewDto>> GetReviewsByGameAsync(int gameId)
        {
            var reviews = await _reviewRepository.GetByGameAsync(gameId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task CreateReviewAsync(CreateReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            await _reviewRepository.AddAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateReviewAsync(int id, UpdateReviewDto reviewDto)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            _mapper.Map(reviewDto, review);
            await _reviewRepository.UpdateAsync(review);
        }
    }
}
