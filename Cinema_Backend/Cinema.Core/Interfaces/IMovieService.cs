﻿using Cinema.Core.DTOs;
using Cinema.Core.Helpers;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Models;

namespace Cinema.Core.Interfaces;

public interface IMovieService
{
    public Task<Result<ICollection<MovieMinimalDto>>> GetAllCurrentMovies();
    
    Task<Result<MovieMinimalDto>> CreateMovie(CreateMovieDto createMovieDto);
    Task<Result<MovieDetailsDto>> GetMovieDetails(Guid id);
}