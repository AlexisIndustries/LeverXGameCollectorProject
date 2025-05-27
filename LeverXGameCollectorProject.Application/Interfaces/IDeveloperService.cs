using LeverXGameCollectorProject.Application.DTOs.Developer;


namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync();
        Task<DeveloperDto> GetDeveloperByIdAsync(int id);
        Task CreateDeveloperAsync(CreateDeveloperDto developerDto);
        Task UpdateDeveloperAsync(int id, UpdateDeveloperDto developerDto);
        Task DeleteDeveloperAsync(int id);
    }
}
