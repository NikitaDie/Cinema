using Cinema.Core.DTOs;
using FluentValidation;

namespace Cinema.Core.Validators.Movie;

public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
{
    public CreateMovieDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Movie name is required.")
            .MaximumLength(100).WithMessage("Film name cannot exceed 100 characters.");

        RuleFor(x => x.AgeRating)
            .InclusiveBetween(1, 18).WithMessage("Rating must be a valid age restriction (1-18).");

        RuleFor(x => x.ReleaseYear)
            .InclusiveBetween(1888, DateTime.UtcNow.Year)
            .WithMessage("Year must be a valid year.");

        RuleFor(x => x.Director)
            .NotEmpty().WithMessage("Director name is required.")
            .MaximumLength(100).WithMessage("Director name cannot exceed 100 characters.");

        RuleFor(x => x.RentalPeriodStart)
            .LessThan(x => x.RentalPeriodEnd).WithMessage("Rental period start date must be before the end date.");

        RuleFor(x => x.Language)
            .NotEmpty().WithMessage("Language is required.")
            .MaximumLength(50).WithMessage("Language cannot exceed 50 characters.");

        RuleFor(x => x.Genres)
            .NotEmpty().WithMessage("Genres are required.")
            .Must(genres => genres.Count > 0).WithMessage("At least one genre ID must be specified.");

        RuleFor(x => x.Duration)
            .InclusiveBetween(TimeSpan.FromMinutes(1), TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59)))
            .WithMessage("Duration must be between 1 minute and 23 hours 59 minutes.");

        RuleFor(x => x.TrailerLink)
            .NotEmpty().WithMessage("Trailer link is required.")
            .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
            .WithMessage("Trailer link must be a valid URL.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image is required.");

        // Optional fields with length validations
        RuleFor(x => x.OriginalTitle)
            .MaximumLength(100).WithMessage("Original name cannot exceed 100 characters.");

        RuleFor(x => x.Producer)
            .MaximumLength(100).WithMessage("Producer name cannot exceed 100 characters.");

        RuleFor(x => x.ProductionStudio)
            .MaximumLength(100).WithMessage("Production studio cannot exceed 100 characters.");

        RuleFor(x => x.Screenplay)
            .MaximumLength(200).WithMessage("Screenplay cannot exceed 200 characters.");

        RuleFor(x => x.InclusiveAdaptation)
            .MaximumLength(200).WithMessage("Inclusive adaptation details cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
    }
}