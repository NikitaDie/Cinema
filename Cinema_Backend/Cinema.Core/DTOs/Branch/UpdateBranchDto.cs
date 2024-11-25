using Cinema.Core.Helpers.Validation;

namespace Cinema.Core.DTOs.Branch;

public class UpdateBranchDto : BaseValidationModel<UpdateBranchDto>
{
    public string? Name { get; set; }

    public string? Address { get; set; }
    
    public string? City { get; set; }
    
    public string? Region { get; set; }
    
    public string? ZipCode { get; set; }
    
    public string? PhoneNumber { get; set; }
}