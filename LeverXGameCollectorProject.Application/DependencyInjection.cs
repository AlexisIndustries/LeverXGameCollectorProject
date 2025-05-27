using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeverXGameCollectorProject.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
