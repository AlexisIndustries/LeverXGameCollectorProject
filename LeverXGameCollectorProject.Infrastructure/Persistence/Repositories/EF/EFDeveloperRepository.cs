using AutoMapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFDeveloperRepository : IDeveloperRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EFDeveloperRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Developer developerEntity)
        {
            var entity = _mapper.Map<DeveloperEntity>(developerEntity);
            await _context.Developers.AddAsync(entity);
            await _context.SaveChangesAsync();
            developerEntity.Id = entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Developers.FindAsync(id);
            if (entity != null)
            {
                _context.Developers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            var entities = await _context.Developers.AsNoTracking()
            .ToListAsync();

            return entities.Select(_mapper.Map<Developer>);
        }

        public async Task<Developer> GetByIdAsync(int id)
        {
            var entity = await _context.Developers.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<Developer>(entity);
        }

        public async Task UpdateAsync(Developer developerEntity)
        {
            var entity = _mapper.Map<DeveloperEntity>(developerEntity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
