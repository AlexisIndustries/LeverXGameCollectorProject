using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using LeverXGameCollectorProject.Domain.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreResponseModel>>
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GetAllGenreQueryHandler(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreResponseModel>> Handle(GetAllGenresQuery request, CancellationToken ct)
        {
            var genres = await _repository.GetAllAsync();
            return genres.Select(_mapper.Map<GenreResponseModel>);
        }
    }
}
