using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Models;


namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IDeveloperService
    {
        Task<IEnumerable<Developer>> GetAllDevelopersAsync();
        Task<Developer> GetDeveloperByIdAsync(int id);
        Task<int> CreateDeveloperAsync(CreateDeveloperRequestModel developerDto);
        Task UpdateDeveloperAsync(int id, UpdateDeveloperRequestModel developerDto);
        Task DeleteDeveloperAsync(int id);
    }
}
