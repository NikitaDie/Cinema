using Cinema.Core.DTOs.Branch;
using Cinema.Core.Interfaces;
using Cinema.Core.RequestFiltering;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/branches")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }
    
    //Get api/branches/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBranch([FromRoute] Guid id)
    {
        var result = await _branchService.GetBranch(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // GET: api/branches
    [HttpGet]
    public async Task<IActionResult> GetAllBranches(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] string? city,
        [FromQuery] string? region,
        [FromQuery] string? zipCode,
        [FromQuery] string? phoneNumber,
        [FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var filter = new BranchFilter
        {
            Name = name,
            Address = address,
            City = city,
            Region = region,
            ZipCode = zipCode,
            PhoneNumber = phoneNumber
        };
        
        var result = await _branchService.GetAllBranches(filter, skip, take);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // POST: api/branches/{id}
    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchDto newBranch)
    {
        var result = await _branchService.CreateBranch(newBranch);
        var createdBranchId = result.Data?.Id;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetBranch), new { id = createdBranchId }, result)
            : NotFound(result.Error);
    }
    
    // PUT: api/branches/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBranch([FromRoute] Guid id, [FromBody] UpdateBranchDto branchToUpdate)
    {
        var result = await _branchService.UpdateBranch(id, branchToUpdate);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // DELETE: api/branches/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBranch([FromRoute] Guid id)
    {
        var result = await _branchService.DeleteBranch(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
}
