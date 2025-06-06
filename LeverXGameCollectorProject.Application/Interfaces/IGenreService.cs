using LeverXGameCollectorProject.Application.DTOs.Genre;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponseModel>> GetAllGenresAsync();
        Task<GenreResponseModel> GetGenreByIdAsync(int id);
        Task CreateGenreAsync(CreateGenreRequestModel genreDto);
        Task UpdateGenreAsync(int id, UpdateGenreResponseModel genreDto);
        Task DeleteGenreAsync(int id);
    }
}
