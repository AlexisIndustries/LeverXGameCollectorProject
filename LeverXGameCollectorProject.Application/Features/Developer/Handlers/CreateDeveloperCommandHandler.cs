using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, Unit>
    {
        private readonly IDeveloperRepository _repository;
        private readonly IMapper _mapper;

        public CreateDeveloperCommandHandler(IDeveloperRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateDeveloperCommand request, CancellationToken ct)
        {
            var developer = _mapper.Map<Models.Developer>(request);
            await _repository.AddAsync(developer);
            return Unit.Value;
        }
    }
}
