using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand, Unit>
    {
        private readonly IDeveloperRepository _repository;
        private readonly IMapper _mapper;

        public DeleteDeveloperCommandHandler(IDeveloperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
