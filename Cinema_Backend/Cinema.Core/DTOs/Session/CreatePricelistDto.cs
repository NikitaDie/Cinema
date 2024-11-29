using Cinema.Core.Helpers.Validation;

namespace Cinema.Core.DTOs.Session;

public class CreatePricelistDto : BaseValidationModel<CreatePricelistDto>
{
    public Guid StatusId { get; set; }
    public decimal Price { get; set; }
}