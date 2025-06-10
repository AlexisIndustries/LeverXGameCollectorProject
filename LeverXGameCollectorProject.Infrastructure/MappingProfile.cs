using AutoMapper;
using LeverXGameCollectorProject.Domain.Persistence.Entities;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeveloperEntity, Developer>().ReverseMap();
            CreateMap<GameEntity, Game>().ReverseMap();
            CreateMap<GenreEntity, Genre>().ReverseMap();
            CreateMap<PlatformEntity, Platform>().ReverseMap();
            CreateMap<ReviewEntity, Review>().ReverseMap();
        }
    }
}
