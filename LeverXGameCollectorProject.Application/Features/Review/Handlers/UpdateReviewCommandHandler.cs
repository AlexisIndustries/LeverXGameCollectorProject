using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Review.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IReviewService _service;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            await _service.UpdateReviewAsync(command.Id, command.Request);
            return Unit.Value;
        }
    }
}
