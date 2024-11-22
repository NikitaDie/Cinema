using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Models;

namespace Cinema.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore()) // Set explicitly after upload
            .ForMember(dest => dest.Genres, opt => opt.Ignore())    // Handled separately
            .ForMember(dest => dest.Starring, opt => opt.Ignore()); // Handled separately
        
        CreateMap<Movie, MovieDetailsDto>();
    }
}