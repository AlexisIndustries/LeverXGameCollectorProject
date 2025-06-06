using AutoMapper;
using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Entities;
using LeverXGameCollectorProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class EFGenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public EFGenreRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Genre genreEntity)
        {
            var entity = _mapper.Map<GenreEntity>(genreEntity);
            await _context.Genres.AddAsync(entity);
            await _context.SaveChangesAsync();
            genreEntity.Id = entity.Id;
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

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var entities = await _context.Genres.AsNoTracking()
            .ToListAsync();

            return entities.Select(_mapper.Map<Genre>);
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var entity = await _context.Genres.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<Genre>(entity);
        }

        public async Task UpdateAsync(Genre genreEntity)
        {
            var entity = _mapper.Map<GenreEntity>(genreEntity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
