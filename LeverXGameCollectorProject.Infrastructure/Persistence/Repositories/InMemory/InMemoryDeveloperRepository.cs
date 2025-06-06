using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory
{
    public class InMemoryDeveloperRepository : IDeveloperRepository
    {
        private static List<Developer> _developers = new()
        {
            new Developer
            {
                Id = 1,
                Name = "CD Projekt Red",
                Country = "Poland",
                Website = "https://cdprojektred.com",
                Founded = new DateTime(1994, 5, 1)
            },
            new Developer
            {
                Id = 2,
                Name = "Nintendo",
                Country = "Japan",
                Website = "https://nintendo.com",
                Founded = new DateTime(1889, 9, 23)
            }
        };

        public Task<Developer> GetByIdAsync(int id)
            => Task.FromResult(_developers.FirstOrDefault(d => d.Id == id));

        public Task<IEnumerable<Developer>> GetAllAsync()
            => Task.FromResult(_developers.AsEnumerable());

        public Task AddAsync(Developer developer)
        {
            developer.Id = _developers.Max(d => d.Id) + 1;
            _developers.Add(developer);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Developer entity)
        {
            var index = _developers.FindIndex(d => d.Id == entity.Id);
            if (index >= 0) _developers[index] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            _developers.RemoveAll(d => d.Id == id);
            return Task.CompletedTask;
        }
    }
}