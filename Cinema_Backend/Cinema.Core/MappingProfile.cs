using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.DTOs.Auditorium;
using Cinema.Core.DTOs.Branch;
using Cinema.Core.DTOs.Status;
using Cinema.Core.Models;

namespace Cinema.Core;

public class MappingProfile : Profile
{ 
    public MappingProfile()
    {
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image.FileName))
            .ForMember(dest => dest.Genres, opt => opt.Ignore())    // Handled separately
            .ForMember(dest => dest.Starring, opt => opt.Ignore()); // Handled separately
        
        CreateMap<Movie, MovieMinimalDto>()
            .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => src.ImagePath));
        
        CreateMap<Movie, MovieDetailsDto>()
            .ForMember(dest => dest.ImageUri, opt => opt.MapFrom(src => src.ImagePath));
        
        CreateMap<Genre, GetGenreDto>();
        
        CreateMap<Actor, GetActorDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        //Branch
        CreateMap<CreateBranchDto, Branch>();
        CreateMap<Branch, GetBranchDto>();
        CreateMap<UpdateBranchDto, Branch>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        //Status
        CreateMap<Status, GetStatusDto>();
        CreateMap<CreateStatusDto, Status>();
        CreateMap<UpdateStatusDto, Status>();
        
        //Auditorium
        CreateMap<CreateAuditoriumDto, Auditorium>();
        CreateMap<Auditorium, AuditoriumMinimalDto>();
        CreateMap<Auditorium, AuditoriumDetailsDto>();
        
        //Seat
        CreateMap<Seat, GetSeatDto>()
            .ForMember(dest => dest.Stasus, opt => opt.MapFrom(src => src.Status.Name));
        CreateMap<CreateSeatDto, Seat>();
    }
}