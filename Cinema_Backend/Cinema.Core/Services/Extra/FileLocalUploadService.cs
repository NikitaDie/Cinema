using Cinema.Core.Interfaces.Extra;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Cinema.Core.Services.Extra;

public class FileLocalUploadService : IFileUploadService
{
    private readonly string _uploadsDirectory;
    private readonly string _moviesUploadsDirectoryExtension;
    private const int MaxFileSizeMb = 25;
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp"];
    private const long MaxFileSize = MaxFileSizeMb * 1024 * 1024; // 5 MB

    public FileLocalUploadService(IConfiguration configuration)
    {
        _uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), configuration["UploadsDirectory"] ?? "public");
        _moviesUploadsDirectoryExtension = configuration["MoviesUploadsDirectoryExtension"] ?? "movies";
    }
    
    private static string GenerateUniqueFileName(IFormFile file)
    {
        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var fileExtension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}{fileExtension}";

        return uniqueFileName;
    }
    
    public async Task<string> UploadFile(IFormFile file)
    {
        ValidateFile(file);

        var fileDirectory = Path.Combine(_uploadsDirectory, _moviesUploadsDirectoryExtension);
        
        // Ensure the directory exists
        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

        var uniqueFileName = GenerateUniqueFileName(file);
        var filePath = Path.Combine(fileDirectory, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        return uniqueFileName;
    }
    
    public string GetFileUrl(string fileName)
        => $"http://localhost:5054/uploads/{_moviesUploadsDirectoryExtension}/{fileName}";
    
    private static void ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }
        
        if (file.Length > MaxFileSize)
        {
            throw new ArgumentException($"File size exceeds the {MaxFileSizeMb} MB limit.");
        }
        
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!AllowedExtensions.Contains(fileExtension))
        {
            throw new ArgumentException("Invalid file type. Only image files are allowed.");
        }
    }
}