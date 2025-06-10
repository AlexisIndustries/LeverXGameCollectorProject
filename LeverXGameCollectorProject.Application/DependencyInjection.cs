using LeverXGameCollectorProject.Application.Interfaces;
using LeverXGameCollectorProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;

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
