using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewByGameIdQueryHandler : IRequestHandler<GetReviewByGameIdQuery, ReviewResponseModel>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public GetReviewByGameIdQueryHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<ReviewResponseModel> Handle(GetReviewByGameIdQuery request, CancellationToken ct)
        {
            var review = await _service.GetReviewsByGameAsync(request.Id);
            return _mapper.Map<ReviewResponseModel>(review);
        }
    }
}
