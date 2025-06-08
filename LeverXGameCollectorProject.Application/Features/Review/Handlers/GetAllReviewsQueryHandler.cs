using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewQueryHandler : IRequestHandler<GetAllReviewQuery, IEnumerable<ReviewResponseModel>>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public GetReviewQueryHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewResponseModel>> Handle(GetAllReviewQuery request, CancellationToken ct)
        {
            var developers = await _service.GetAllReviewsAsync();
            return developers.Select(_mapper.Map<ReviewResponseModel>);
        }
    }
}
