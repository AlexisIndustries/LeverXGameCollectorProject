using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFGenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public EFGenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(GenreEntity genreEntity)
        {
            var ent = await _context.Genres.AddAsync(genreEntity);
            await _context.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Genres.FindAsync(id);
            if (entity != null)
            {
                _context.Genres.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GenreEntity>> GetAllAsync()
        {
            var entities = await _context.Genres.AsNoTracking()
            .ToListAsync();

            return entities;
        }

        public async Task<GenreEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Genres.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

            return entity;
        }

        public async Task UpdateAsync(GenreEntity genreEntity)
        {
            _context.Entry(genreEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
