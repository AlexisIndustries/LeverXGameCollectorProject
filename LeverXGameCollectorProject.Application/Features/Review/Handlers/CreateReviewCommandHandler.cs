using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Unit>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateReviewCommand request, CancellationToken ct)
        {
            var review = _mapper.Map<CreateReviewRequestModel>(request);
            await _service.CreateReviewAsync(review);
            return Unit.Value;
        }
    }
}
