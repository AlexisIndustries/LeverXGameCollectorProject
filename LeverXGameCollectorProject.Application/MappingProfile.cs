using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Models;

namespace LeverXGameCollectorProject.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Game, GameDto>();

            CreateMap<CreateGameDto, Game>();

            CreateMap<UpdateGameDto, Game>();

            CreateMap<Developer, DeveloperDto>();

            CreateMap<CreateDeveloperDto, Developer>();
            
            CreateMap<UpdateDeveloperDto, Developer>();

            CreateMap<Genre, GenreDto>();

            CreateMap<CreateGenreDto, Genre>();

            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Review, ReviewDto>();

            CreateMap<CreateReviewDto, Review>();

            CreateMap<UpdateReviewDto, Review>();

            CreateMap<Platform, PlatformDto>();

            CreateMap<CreatePlatformDto, Platform>();

            CreateMap<UpdatePlatformDto, Platform>();

        }
    }
}
