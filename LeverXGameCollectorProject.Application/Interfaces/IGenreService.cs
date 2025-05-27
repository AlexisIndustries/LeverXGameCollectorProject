using LeverXGameCollectorProject.Application.DTOs.Genre;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(int id);
        Task CreateGenreAsync(CreateGenreDto genreDto);
        Task UpdateGenreAsync(int id, UpdateGenreDto genreDto);
        Task DeleteGenreAsync(int id);
    }
}
