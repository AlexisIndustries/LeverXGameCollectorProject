using AutoMapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFGameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EFGameRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Game gameEntity)
        {
            var entity = _mapper.Map<GameEntity>(gameEntity);
            entity.Developer = await _context.Developers.FindAsync(gameEntity.Developer.Id);
            entity.Platform = await _context.Platforms.FindAsync(gameEntity.Platform.Id);
            entity.Genre = await _context.Genres.FindAsync(gameEntity.Genre.Id);
            await _context.Games.AddAsync(entity);
            await _context.SaveChangesAsync();
            gameEntity.Id = entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Games.FindAsync(id);
            if (entity != null)
            {
                _context.Games.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var entities = await _context.Games.AsNoTracking()
            .Include(g => g.Platform)
            .Include(g => g.Developer)
            .Include(g => g.Genre)
            .ToListAsync();

            return entities.Select(_mapper.Map<Game>);
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            var entity = await _context.Games.AsNoTracking()
                .Include(g => g.Platform)
                .Include(g => g.Developer)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Game>(entity);
        }

        public async Task<IEnumerable<Game>> GetByPlatformAsync(int platformId)
        {
            var entities = await _context.Games.AsNoTracking()
            .Where(g => g.Platform.Id == platformId)
            .Include(g => g.Platform)
            .ToListAsync();

            return entities.Select(_mapper.Map<Game>);
        }

        public async Task UpdateAsync(Game gameEntity)
        {
            var entity = _mapper.Map<GameEntity>(gameEntity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
