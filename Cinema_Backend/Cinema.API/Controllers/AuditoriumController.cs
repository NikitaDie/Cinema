using Cinema.Core.DTOs.Auditorium;
using Cinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/auditoriums")]
[ApiController]
public class AuditoriumController : ControllerBase
{
    private readonly IAuditoriumService _auditoriumService;

    public AuditoriumController(IAuditoriumService auditoriumService)
    {
        _auditoriumService = auditoriumService;
    }
    
     //Get api/auditoriums/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuditorium([FromRoute] Guid id)
    {
        var result = await _auditoriumService.GetAuditorium(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // // GET: api/auditoriums
    // [HttpGet]
    // public async Task<IActionResult> GetAllAuditoriums(
    //     [FromQuery] string? name,
    //     [FromQuery] string? address,
    //     [FromQuery] string? city,
    //     [FromQuery] string? region,
    //     [FromQuery] string? zipCode,
    //     [FromQuery] string? phoneNumber,
    //     [FromQuery] int skip = 0, [FromQuery] int take = 10)
    // {
    //     var filter = new BranchFilter
    //     {
    //         Name = name,
    //         Address = address,
    //         City = city,
    //         Region = region,
    //         ZipCode = zipCode,
    //         PhoneNumber = phoneNumber
    //     };
    //     
    //     var result = await _auditoriumService.GetAllBranches(filter, skip, take);
    //     return result.IsSuccess
    //         ? Ok(result)
    //         : NotFound(result.Error);
    // }
    
    // POST: api/branches/{id}
    [HttpPost]
    public async Task<IActionResult> CreateAuditorium([FromBody] CreateAuditoriumDto newAuditorium)
    {
        var result = await _auditoriumService.CreateAuditorium(newAuditorium);
        var createdAuditoriumId = result.Data?.Id;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetAuditorium), new { id = createdAuditoriumId }, result)
            : NotFound(result.Error);
    }
    
    // // PUT: api/branches/{id}
    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> UpdateBranch([FromRoute] Guid id, [FromBody] UpdateBranchDto branchToUpdate)
    // {
    //     var result = await _branchService.UpdateBranch(id, branchToUpdate);
    //     return result.IsSuccess
    //         ? Ok(result)
    //         : NotFound(result.Error);
    // }
    //
    // // DELETE: api/branches/{id}
    // [HttpDelete("{id:guid}")]
    // public async Task<IActionResult> DeleteBranch([FromRoute] Guid id)
    // {
    //     var result = await _branchService.DeleteBranch(id);
    //     return result.IsSuccess
    //         ? Ok(result)
    //         : NotFound(result.Error);
    // }
}