using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace LeverXGameCollectorProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string type)
        {
            if (type == "DAPPER")
            {
                services.AddScoped<IGameRepository, DapperGameRepository>();
                services.AddScoped<IReviewRepository, DapperReviewRepository>();
                services.AddScoped<IDeveloperRepository, DapperDeveloperRepository>();
                services.AddScoped<IGenreRepository, DapperGenreRepository>();
                services.AddScoped<IPlatformRepository, DapperPlatformRepository>();
            } else
            {
                services.AddScoped<IGameRepository, EFGameRepository>();
                services.AddScoped<IReviewRepository, EFReviewRepository>();
                services.AddScoped<IDeveloperRepository, EFDeveloperRepository>();
                services.AddScoped<IGenreRepository, EFGenreRepository>();
                services.AddScoped<IPlatformRepository, EFPlatformRepository>();
            }
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {
            services.AddSingleton<IGameRepository, InMemoryGameRepository>();
            services.AddSingleton<IReviewRepository, InMemoryReviewRepository>();
            services.AddSingleton<IDeveloperRepository, InMemoryDeveloperRepository>();
            services.AddSingleton<IGenreRepository, InMemoryGenreRepository>();
            services.AddSingleton<IPlatformRepository, InMemoryPlatformRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
