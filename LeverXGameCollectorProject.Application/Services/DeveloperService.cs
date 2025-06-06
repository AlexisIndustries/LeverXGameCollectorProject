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

        public async Task<IEnumerable<DeveloperResponseModel>> GetAllDevelopersAsync()
        {
            var developers = await _developerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeveloperResponseModel>>(developers);
        }

        public async Task<DeveloperResponseModel> GetDeveloperByIdAsync(int id)
        {
            var developer = await _developerRepository.GetByIdAsync(id);
            return _mapper.Map<DeveloperResponseModel>(developer);
        }

        public async Task CreateDeveloperAsync(CreateDeveloperRequestModel developerDto)
        {
            var developer = _mapper.Map<Developer>(developerDto);
            await _developerRepository.AddAsync(developer);
        }

        public async Task UpdateDeveloperAsync(int id, UpdateDeveloperRequestModel developerDto)
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
