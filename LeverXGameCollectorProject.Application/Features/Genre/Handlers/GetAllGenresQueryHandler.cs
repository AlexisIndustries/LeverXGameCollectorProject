using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Genre.Handlers
{
    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreResponseModel>>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public GetAllGenreQueryHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreResponseModel>> Handle(GetAllGenresQuery request, CancellationToken ct)
        {
            var genres = await _service.GetAllGenresAsync();
            return genres.Select(_mapper.Map<GenreResponseModel>);
        }
    }
}
