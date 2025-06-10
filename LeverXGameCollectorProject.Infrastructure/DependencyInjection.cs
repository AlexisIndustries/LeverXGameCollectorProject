using LeverXGameCollectorProject.Domain;
using LeverXGameCollectorProject.Application.Repositories.Interfaces;
using LeverXGameCollectorProject.Domain.Entities.DB;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.Dapper;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.EF;
using LeverXGameCollectorProject.Infrastructure.Persistence.Repositories.InMemory;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LeverXGameCollectorProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, RepositoryType type)
        {
            if (type == RepositoryType.Dapper)
            {
                services.AddScoped<IDeveloperRepository, DapperDeveloperRepository>();
                services.AddScoped<IReviewRepository, DapperReviewRepository>();
                services.AddScoped<IGameRepository, DapperGameRepository>();
                services.AddScoped<IGenreRepository, DapperGenreRepository>();
                services.AddScoped<IPlatformRepository, DapperPlatformRepository>();
            } else
            {
                services.AddScoped<IDeveloperRepository, EFDeveloperRepository>();
                services.AddScoped<IReviewRepository, EFReviewRepository>();
                services.AddScoped<IGameRepository, EFGameRepository>();
                services.AddScoped<IGenreRepository, EFGenreRepository>();
                services.AddScoped<IPlatformRepository, EFPlatformRepository>();
            }
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {
            services.AddSingleton<IDeveloperRepository, InMemoryDeveloperRepository>();
            services.AddSingleton<IReviewRepository, InMemoryReviewRepository>();
            services.AddSingleton<IGameRepository, InMemoryGameRepository>();
            services.AddSingleton<IGenreRepository, InMemoryGenreRepository>();
            services.AddSingleton<IPlatformRepository, InMemoryPlatformRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
