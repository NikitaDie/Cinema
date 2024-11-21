﻿using System.ComponentModel.DataAnnotations;
using Cinema.Core.Helpers;
using Cinema.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Cinema.Core.DTOs;

public class CreateMovieDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Film name cannot exceed 100 characters.")]
    public string Title { get; set; }

    [Required]
    [Range(1, 18, ErrorMessage = "Rating must be a valid age restriction (1-18).")]
    public int AgeRating { get; set; }

    [Required]
    [Range(1888, int.MaxValue, ErrorMessage = "Year must be a valid year.")]
    public int ReleaseYear { get; set; }

    [StringLength(100, ErrorMessage = "Original name cannot exceed 100 characters.")]
    public string? OriginalTitle { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Director name cannot exceed 100 characters.")]
    public string Director { get; set; }

    [Required]
    public DateTime RentalPeriodStart { get; set; }

    [Required]
    public DateTime RentalPeriodEnd { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Language cannot exceed 50 characters.")]
    public string Language { get; set; }

    [Required]
    public List<string> Genres { get; set; }

    [Required(ErrorMessage = "Duration is required.")]
    [Range(typeof(TimeSpan), "00:01", "23:59", ErrorMessage = "Duration must be between 1 minute and 23 hours 59 minutes.")]
    public TimeSpan Duration { get; set; }
    
    [StringLength(100, ErrorMessage = "Producer name cannot exceed 100 characters.")]
    public string? Producer { get; set; }

    [StringLength(100, ErrorMessage = "Production studio cannot exceed 100 characters.")]
    public string? ProductionStudio { get; set; }

    [StringLength(200, ErrorMessage = "Screenplay cannot exceed 200 characters.")]
    public string? Screenplay { get; set; }
    
    public List<string>? Starring { get; set; }

    [StringLength(200, ErrorMessage = "Inclusive adaptation details cannot exceed 200 characters.")]
    public string? InclusiveAdaptation { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    [Required]
    [Url(ErrorMessage = "Trailer link must be a valid URL.")]
    public string TrailerLink { get; set; }

    [Required(ErrorMessage = "Image is required.")]
    public IFormFile Image { get; set; }
    
    public ServiceResult Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Title))
            errors.Add("Movie name is required.");

        if (AgeRating <= 0)
            errors.Add("Rating must be a positive number.");

        if (ReleaseYear <= 1900 || ReleaseYear > DateTime.UtcNow.Year)
            errors.Add("Year must be a valid year.");

        // Validate rental period
        if (RentalPeriodStart == null || RentalPeriodEnd == null)
            errors.Add("Rental period start and end dates are required.");

        if (RentalPeriodStart >= RentalPeriodEnd)
            errors.Add("Rental period start date must be before the end date.");

        if (RentalPeriodStart <= DateTime.UtcNow)
            errors.Add("Rental period start date must be in the future.");

        // Validate trailer link URL (if present)
        if (!string.IsNullOrEmpty(TrailerLink) && !Uri.IsWellFormedUriString(TrailerLink, UriKind.Absolute))
            errors.Add("Trailer link must be a valid URL.");

        return errors.Count > 0 ? new ServiceResult(false, string.Join(",", errors))
            : new ServiceResult(true);

    }
    
    
}