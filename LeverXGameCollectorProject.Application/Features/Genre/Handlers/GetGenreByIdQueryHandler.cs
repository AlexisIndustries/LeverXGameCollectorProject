using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreResponseModel>
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryHandler(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenreResponseModel> Handle(GetGenreByIdQuery request, CancellationToken ct)
        {
            var Genre = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<GenreResponseModel>(Genre);
        }
    }
}
