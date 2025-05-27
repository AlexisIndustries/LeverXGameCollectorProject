using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task<GenreDto> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task CreateGenreAsync(CreateGenreDto GenreDto)
        {
            var genre = _mapper.Map<Genre>(GenreDto);
            await _genreRepository.AddAsync(genre);
        }

        public async Task UpdateGenreAsync(int id, UpdateGenreDto GenreDto)
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
