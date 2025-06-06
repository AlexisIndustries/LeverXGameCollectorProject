using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewByGameIdQueryHandler : IRequestHandler<GetReviewByGameIdQuery, ReviewResponseModel>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public GetReviewByGameIdQueryHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReviewResponseModel> Handle(GetReviewByGameIdQuery request, CancellationToken ct)
        {
            var review = await _repository.GetByGameAsync(request.Id);
            return _mapper.Map<ReviewResponseModel>(review);
        }
    }
}
