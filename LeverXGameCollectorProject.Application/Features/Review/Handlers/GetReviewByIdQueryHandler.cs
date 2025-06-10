using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewResponseModel>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<ReviewResponseModel> Handle(GetReviewByIdQuery request, CancellationToken ct)
        {
            var review = await _service.GetReviewByIdAsync(request.Id);
            return _mapper.Map<ReviewResponseModel>(review);
        }
    }
}
