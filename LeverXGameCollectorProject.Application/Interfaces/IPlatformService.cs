using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
        Task<PlatformDto> GetPlatformByIdAsync(int id);
        Task CreatePlatformAsync(CreatePlatformDto PlatformDto);
        Task UpdatePlatformAsync(int id, UpdatePlatformDto PlatformDto);
        Task DeletePlatformAsync(int id);
    }
}
