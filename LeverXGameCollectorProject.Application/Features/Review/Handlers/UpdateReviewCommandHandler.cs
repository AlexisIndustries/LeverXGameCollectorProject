using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(_mapper.Map<Models.Review>(request));
            return Unit.Value;
        }
    }
}
