using LeverXGameCollectorProject.Domain.Entities.DB;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DeveloperEntity> Developers { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<PlatformEntity> Platforms { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
    }
}
