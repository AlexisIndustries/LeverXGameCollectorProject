using AutoMapper;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Unit>
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(_mapper.Map<Models.Genre>(request));
            return Unit.Value;
        }
    }
}
