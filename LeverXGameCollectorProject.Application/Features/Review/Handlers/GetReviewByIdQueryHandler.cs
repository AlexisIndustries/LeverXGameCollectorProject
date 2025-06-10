using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Models.Review>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Models.Review> Handle(GetReviewByIdQuery query, CancellationToken ct)
        {
            return await _service.GetReviewByIdAsync(query.Id);
        }
    }
}
