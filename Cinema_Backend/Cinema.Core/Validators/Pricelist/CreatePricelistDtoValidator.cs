using Cinema.Core.DTOs.Session;
using FluentValidation;

namespace Cinema.Core.Validators.Pricelist;

public class CreatePricelistDtoValidator: AbstractValidator<CreatePricelistDto>
{
    public CreatePricelistDtoValidator()
    {
        RuleFor(x => x.StatusId)
            .NotEmpty()
            .WithMessage("StatusId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("StatusId cannot be an empty GUID.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}