using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository GenreRepository, IMapper mapper)
        {
            _genreRepository = GenreRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Genre>>(genres);
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return _mapper.Map<Genre>(genre);
        }

        public async Task<int> CreateGenreAsync(CreateGenreRequestModel GenreDto)
        {
            var genre = _mapper.Map<GenreEntity>(GenreDto);
            var id = await _genreRepository.AddAsync(genre);
            return id;
        }

        public async Task UpdateGenreAsync(int id, UpdateGenreRequestModel GenreDto)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            _mapper.Map(GenreDto, genre);
            await _genreRepository.UpdateAsync(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            await _genreRepository.DeleteAsync(id);
        }   
    }
}
