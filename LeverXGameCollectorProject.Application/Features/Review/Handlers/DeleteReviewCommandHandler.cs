using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteReviewAsync(request.Id);
            return Unit.Value;
        }
    }
}
