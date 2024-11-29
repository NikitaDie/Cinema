using Cinema.Core.DTOs.Status;

namespace Cinema.Core.DTOs.Session;

public class GetPricelistDto
{
    public Guid Id { get; set; }

    public GetStatusDto Status { get; set; }

    public decimal Price { get; set; }
}