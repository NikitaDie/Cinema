using Cinema.Core.DTOs.Branch;
using FluentValidation;

namespace Cinema.Core.Validators.Branch;

public class UpdateBranchDtoValidator : AbstractValidator<UpdateBranchDto>
{
    public UpdateBranchDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.Name)); // Only validate if Name is provided

        RuleFor(x => x.Address)
            .MaximumLength(255).WithMessage("Address cannot exceed 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.Address)); // Only validate if Address is provided

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.City)); // Only validate if City is provided

        RuleFor(x => x.Region)
            .MaximumLength(100).WithMessage("Region cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Region)); // Only validate if Region is provided

        RuleFor(x => x.ZipCode)
            .MaximumLength(20).WithMessage("ZipCode cannot exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.ZipCode)); // Only validate if ZipCode is provided

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits and may include a leading '+'.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber)); // Only validate if PhoneNumber is provided
    }
}