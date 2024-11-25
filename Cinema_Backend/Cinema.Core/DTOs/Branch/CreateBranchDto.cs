using Cinema.Core.Helpers.Validation;

namespace Cinema.Core.DTOs.Branch;

public class CreateBranchDto : BaseValidationModel<CreateBranchDto>
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;
    
    public string City { get; set; } = null!;
    
    public string Region { get; set; } = null!;
    
    public string ZipCode { get; set; } = null!;
    
    public string PhoneNumber { get; set; } = null!;
}