using LeverXGameCollectorProject.Application.DTOs.Developer;


namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperResponseModel>> GetAllDevelopersAsync();
        Task<DeveloperResponseModel> GetDeveloperByIdAsync(int id);
        Task<int> CreateDeveloperAsync(CreateDeveloperRequestModel developerDto);
        Task UpdateDeveloperAsync(int id, UpdateDeveloperRequestModel developerDto);
        Task DeleteDeveloperAsync(int id);
    }
}
