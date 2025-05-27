using LeverXGameCollectorProject.Domain.Interfaces;
using LeverXGameCollectorProject.Infrastructure.Persistence.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IGameRepository, InMemoryGameRepository>();
            services.AddSingleton<IReviewRepository, InMemoryReviewRepository>();
            services.AddSingleton<IDeveloperRepository, InMemoryDeveloperRepository>();
            services.AddSingleton<IGenreRepository, InMemoryGenreRepository>();
            services.AddSingleton<IPlatformRepository, InMemoryPlatformRepository>();
            return services;
        }
    }
}
