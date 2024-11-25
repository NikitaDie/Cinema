using Cinema.Core.DTOs.Branch;
using FluentValidation;

namespace Cinema.Core.Validators.Branch;

public class CreateBranchDtoValidator : AbstractValidator<CreateBranchDto>
{
    public CreateBranchDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(255).WithMessage("Address cannot exceed 255 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

        RuleFor(x => x.Region)
            .NotEmpty().WithMessage("Region is required.")
            .MaximumLength(100).WithMessage("Region cannot exceed 100 characters.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("ZipCode is required.")
            .MaximumLength(20).WithMessage("ZipCode cannot exceed 20 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits and may include a leading '+'.");
    }
}