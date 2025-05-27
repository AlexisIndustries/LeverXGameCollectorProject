using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs;
using LeverXGameCollectorProject.Application.DTOs.Developer;
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
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DeveloperService(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync()
        {
            var developers = await _developerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeveloperDto>>(developers);
        }

        public async Task<DeveloperDto> GetDeveloperByIdAsync(int id)
        {
            var developer = await _developerRepository.GetByIdAsync(id);
            return _mapper.Map<DeveloperDto>(developer);
        }

        public async Task CreateDeveloperAsync(CreateDeveloperDto developerDto)
        {
            var developer = _mapper.Map<Developer>(developerDto);
            await _developerRepository.AddAsync(developer);
        }

        public async Task UpdateDeveloperAsync(int id, UpdateDeveloperDto developerDto)
        {
            var developer = await _developerRepository.GetByIdAsync(id);
            _mapper.Map(developerDto, developer);
            await _developerRepository.UpdateAsync(developer);
        }

        public async Task DeleteDeveloperAsync(int id)
        {
            await _developerRepository.DeleteAsync(id);
        }
    }
}
