using Cinema.Core.DTOs.Branch;
using Cinema.Core.Models;
using Cinema.Core.RequestFiltering;
using Cinema.Core.Services;

namespace Cinema.Tests.UnitTests;

public class BranchServiceTests : TestConfiguration
{
    private readonly BranchService _branchService;

    public BranchServiceTests()
    {
        _branchService = new BranchService(Repository, Mapper);
    }

    [Fact]
    public async Task GetBranch_ReturnsBranch_WhenBranchExists()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var branch = new Branch { 
            Id = branchId,
            Name = "Test Branch",
            Address = "123 Main Street",
            City = "Test City",
            PhoneNumber = "123-456-7890",
            Region = "Test Region",
            ZipCode = "12345",
            IsDeleted = false };
        await DbContext.Branches.AddAsync(branch);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _branchService.GetBranch(branchId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(branchId, result.Data?.Id);
    }

    [Fact]
    public async Task GetBranch_ReturnsFailure_WhenBranchDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();

        // Act
        var result = await _branchService.GetBranch(branchId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Branch not found.", result.Error);
    }

    [Fact]
    public async Task GetAllBranches_ReturnsFilteredBranches()
    {
        // Arrange
        var branches = new List<Branch>
        {
            new Branch { Id = Guid.NewGuid(),
                Name = "Branch 1",
                Address = "123 Main Street",
                City = "Test City",
                PhoneNumber = "123-456-7890",
                Region = "Test Region",
                ZipCode = "12345",
                IsDeleted = false },
            new Branch { Id = Guid.NewGuid(),
                Name = "Branch 2",
                Address = "123 Main Street",
                City = "Test City",
                PhoneNumber = "123-456-7890",
                Region = "Test Region",
                ZipCode = "12345",
                IsDeleted = false }
        };
        await DbContext.Branches.AddRangeAsync(branches);
        await DbContext.SaveChangesAsync();

        var filter = new BranchFilter { Name = "Branch 1" };

        // Act
        var result = await _branchService.GetAllBranches(filter, 0, 10);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Data ?? new List<GetBranchDto>());
        Assert.Equal("Branch 1", result.Data?.First().Name);
    }

    [Fact]
    public async Task CreateBranch_ReturnsCreatedBranch()
    {
        // Arrange
        var branchDto = new CreateBranchDto
        {
            Name = "New Branch",
            Address = "123 Main Street",
            City = "Sample City",
            Region = "Sample Region",
            ZipCode = "12345",
            PhoneNumber = "123-456-7890"
        };

        // Act
        var result = await _branchService.CreateBranch(branchDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal("New Branch", result.Data?.Name);

        // Verify branch is saved in the database
        var savedBranch = await DbContext.Branches.FindAsync(result.Data?.Id);
        Assert.NotNull(savedBranch);
    }

    [Fact]
    public async Task UpdateBranch_ReturnsUpdatedBranch_WhenBranchExists()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var branch = new Branch { 
            Id = branchId,
            Name = "Test Branch",
            Address = "123 Main Street",
            City = "Test City",
            PhoneNumber = "123-456-7890",
            Region = "Test Region",
            ZipCode = "12345",
            IsDeleted = false };
        await DbContext.Branches.AddAsync(branch);
        await DbContext.SaveChangesAsync();

        var updateDto = new UpdateBranchDto { Name = "Updated Branch" };

        // Act
        var result = await _branchService.UpdateBranch(branchId, updateDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal("Updated Branch", result.Data?.Name);

        // Verify branch is updated in the database
        var updatedBranch = await DbContext.Branches.FindAsync(branchId);
        Assert.Equal("Updated Branch", updatedBranch?.Name);
    }

    [Fact]
    public async Task DeleteBranch_ReturnsSuccess_WhenBranchExists()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var branch = new Branch
        {
            Id = branchId,
            Name = "Test Branch",
            Address = "123 Main Street",
            City = "Test City",
            PhoneNumber = "123-456-7890",
            Region = "Test Region",
            ZipCode = "12345",
            IsDeleted = false
        };
        await DbContext.Branches.AddAsync(branch);
        await DbContext.SaveChangesAsync();

        // Act
        var result = await _branchService.DeleteBranch(branchId);

        // Assert
        Assert.True(result.IsSuccess);

        // Verify branch is marked as deleted
        var deletedBranch = await DbContext.Branches.FindAsync(branchId);
        Assert.True(deletedBranch?.IsDeleted);
    }

    [Fact]
    public async Task DeleteBranch_ReturnsFailure_WhenBranchDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();

        // Act
        var result = await _branchService.DeleteBranch(branchId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Branch not found.", result.Error);
    }
}
