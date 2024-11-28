using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.DTOs.Branch;
using Cinema.Core.Interfaces.Extra;
using Cinema.Core.Models;
using Microsoft.Extensions.Configuration;

namespace Cinema.Core;

public class MappingProfile : Profile
{
    private readonly IFileUploadService _fileUploadService;
    
    public MappingProfile(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
        
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image.FileName))
            .ForMember(dest => dest.Genres, opt => opt.Ignore())    // Handled separately
            .ForMember(dest => dest.Starring, opt => opt.Ignore()); // Handled separately
        
        CreateMap<Movie, MovieMinimalDto>()
            .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => fileUploadService.GetFileUrl(src.ImagePath)));
        
        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => fileUploadService.GetFileUrl(src.ImagePath)));

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