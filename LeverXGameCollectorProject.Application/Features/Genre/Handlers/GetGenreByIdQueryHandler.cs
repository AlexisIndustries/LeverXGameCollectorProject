using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Features.Genre.Queries;
using LeverXGameCollectorProject.Application.Interfaces;
using MediatR;

namespace LeverXGameCollectorProject.Application.Features.Developer.Handlers
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreResponseModel>
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryHandler(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<GenreResponseModel> Handle(GetGenreByIdQuery request, CancellationToken ct)
        {
            var genre = await _service.GetGenreByIdAsync(request.Id);
            return _mapper.Map<GenreResponseModel>(genre);
        }
    }
}
