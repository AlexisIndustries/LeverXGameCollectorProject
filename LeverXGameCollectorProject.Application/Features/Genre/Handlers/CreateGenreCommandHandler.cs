using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Unit>
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateGenreCommand request, CancellationToken ct)
        {
            var genre = _mapper.Map<Models.Genre>(request);
            await _repository.AddAsync(genre);
            return Unit.Value;
        }
    }
}
