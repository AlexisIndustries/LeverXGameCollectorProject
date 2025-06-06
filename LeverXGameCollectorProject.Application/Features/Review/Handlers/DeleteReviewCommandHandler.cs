using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
