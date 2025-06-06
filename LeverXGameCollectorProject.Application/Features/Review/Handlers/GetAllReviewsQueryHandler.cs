using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class GetReviewQueryHandler : IRequestHandler<GetAllReviewQuery, IEnumerable<ReviewResponseModel>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public GetReviewQueryHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewResponseModel>> Handle(GetAllReviewQuery request, CancellationToken ct)
        {
            var developers = await _repository.GetAllAsync();
            return developers.Select(_mapper.Map<ReviewResponseModel>);
        }
    }
}
