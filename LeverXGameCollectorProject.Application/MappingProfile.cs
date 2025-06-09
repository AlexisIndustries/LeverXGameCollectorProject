using AutoMapper;
using LeverXGameCollectorProject.Application.DTOs.Developer;
using LeverXGameCollectorProject.Application.DTOs.Game;
using LeverXGameCollectorProject.Application.DTOs.Genre;
using LeverXGameCollectorProject.Application.DTOs.Platform;
using LeverXGameCollectorProject.Application.DTOs.Review;
using LeverXGameCollectorProject.Application.Features.Developer.Commands;
using LeverXGameCollectorProject.Application.Features.Game.Commands;
using LeverXGameCollectorProject.Application.Features.Genre.Commands;
using LeverXGameCollectorProject.Application.Features.Platform.Commands;
using LeverXGameCollectorProject.Application.Features.Review.Commands;
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
            CreateMap<Game, CreateGameCommand>().ReverseMap();
            CreateMap<Game, UpdateGameCommand>().ReverseMap();

            CreateMap<Developer, DeveloperResponseModel>().ReverseMap();
            CreateMap<CreateDeveloperRequestModel, Developer>().ReverseMap();
            CreateMap<UpdateDeveloperRequestModel, Developer>().ReverseMap();
            CreateMap<CreateDeveloperCommand, Developer>().ReverseMap();
            CreateMap<UpdateDeveloperCommand, Developer>().ReverseMap();

            CreateMap<Genre, GenreResponseModel>().ReverseMap();
            CreateMap<UpdateGenreRequestModel, Genre>().ReverseMap();
            CreateMap<CreateGenreRequestModel, Genre>().ReverseMap();
            CreateMap<UpdateGenreCommand, Genre>().ReverseMap();
            CreateMap<CreateGenreCommand, Genre>().ReverseMap();

            CreateMap<Review, ReviewResponseModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id))
                .ReverseMap();
            CreateMap<Review, CreateReviewRequestModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id))
                .ReverseMap();
            CreateMap<Review, UpdateReviewRequestModel>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id))
                .ReverseMap();
            CreateMap<Review, CreateReviewCommand>().ReverseMap();
            CreateMap<Review, UpdateReviewCommand>().ReverseMap();

            CreateMap<Platform, PlatformResponseModel>().ReverseMap();
            CreateMap<CreatePlatformRequestModel, Platform>().ReverseMap();
            CreateMap<UpdatePlatformRequestModel, Platform>().ReverseMap();
            CreateMap<Platform, CreatePlatformCommand>().ReverseMap();
            CreateMap<Platform, CreatePlatformCommand>().ReverseMap();
        }
    }
}
