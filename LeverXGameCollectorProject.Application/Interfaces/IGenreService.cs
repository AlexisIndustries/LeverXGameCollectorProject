using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<int> CreateGenreAsync(CreateGenreRequestModel genreDto);
        Task UpdateGenreAsync(int id, UpdateGenreRequestModel genreDto);
        Task DeleteGenreAsync(int id);
    }
}
