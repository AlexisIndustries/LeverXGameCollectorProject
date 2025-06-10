using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewQueryHandler : IRequestHandler<GetAllReviewQuery, IEnumerable<Models.Review>>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public GetReviewQueryHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Review>> Handle(GetAllReviewQuery request, CancellationToken ct)
        {
            return await _service.GetAllReviewsAsync();
        }
    }
}
