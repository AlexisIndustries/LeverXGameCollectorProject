using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Unit>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateReviewCommand request, CancellationToken ct)
        {
            var review = _mapper.Map<Models.Review>(request);
            await _repository.AddAsync(review);
            return Unit.Value;
        }
    }
}
