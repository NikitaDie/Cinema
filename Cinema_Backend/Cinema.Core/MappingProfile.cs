using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.DTOs.Branch;
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
        
        CreateMap<Movie, MovieMinimalDto>()
            .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => MapImageUri(src.ImagePath)));
        
        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => MapImageUri(src.ImagePath)));

        CreateMap<Genre, GetGenreDto>();
        
        CreateMap<Actor, GetActorDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        //Branch
        CreateMap<CreateBranchDto, Branch>();
        CreateMap<Branch, GetBranchDto>();
        CreateMap<UpdateBranchDto, Branch>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
    
    private string MapImageUri(string imagePath) //todo: unify
    {
        var fileName = imagePath.Substring(imagePath.IndexOf("public") + "public".Length + 1); // Get path after 'public/'
        
        fileName = fileName.Replace("\\", "/");
        
        return $"http://localhost:5054/uploads/{fileName.Replace("\\", "/")}";
    }
}