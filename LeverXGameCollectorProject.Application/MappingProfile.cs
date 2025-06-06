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
            CreateMap<Game, GameResponseModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Platform.Id))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id))
                .ForMember(dest => dest.DeveloperId, opt => opt.MapFrom(src => src.Developer.Id))
                .ReverseMap();

            CreateMap<Game, CreateGameRequestModel>().
                ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Platform.Id))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id))
                .ForMember(dest => dest.DeveloperId, opt => opt.MapFrom(src => src.Developer.Id))
                .ReverseMap();

            CreateMap<Game, UpdateGameRequestModel>().
                ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Platform.Id))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id))
                .ForMember(dest => dest.DeveloperId, opt => opt.MapFrom(src => src.Developer.Id))
                .ReverseMap();

            CreateMap<Developer, DeveloperResponseModel>().ReverseMap();

            CreateMap<CreateDeveloperRequestModel, Developer>().ReverseMap();
            
            CreateMap<UpdateDeveloperRequestModel, Developer>().ReverseMap();

            CreateMap<Genre, GenreResponseModel>().ReverseMap();

            CreateMap<CreateGenreRequestModel, Genre>().ReverseMap();

            CreateMap<UpdateGenreResponseModel, Genre>().ReverseMap();

            CreateMap<Review, ReviewResponseModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id))
                .ReverseMap();

            CreateMap<Review, CreateReviewRequestModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id))
                .ReverseMap();

            CreateMap<Review, UpdateReviewRequestModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id))
                .ReverseMap();

            CreateMap<Platform, PlatformResponseModel>().ReverseMap();

            CreateMap<CreatePlatformRequestModel, Platform>().ReverseMap();

            CreateMap<UpdatePlatformRequestModel, Platform>().ReverseMap();

        }
    }
}
