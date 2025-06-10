using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewByGameIdQueryHandler : IRequestHandler<GetReviewByGameIdQuery, Models.Review>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public GetReviewByGameIdQueryHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Models.Review> Handle(GetReviewByGameIdQuery query, CancellationToken ct)
        {
            var reviews = await _service.GetReviewsByGameAsync(query.Id);
            return reviews.FirstOrDefault();
        }
    }
}
