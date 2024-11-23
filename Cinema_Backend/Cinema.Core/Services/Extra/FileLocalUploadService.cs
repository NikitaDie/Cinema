using Cinema.Core.Interfaces.Extra;
using Microsoft.AspNetCore.Http;

namespace Cinema.Core.Services.Extra;

public class FileLocalUploadService : IFileUploadService
{
    private static readonly int _maxFileSizeMB = 25;
    private static readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp"];
    private static readonly long _maxFileSize = _maxFileSizeMB * 1024 * 1024; // 5 MB

    private static string GenerateUniqueFileName(IFormFile file)
    {
        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var fileExtension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}{fileExtension}";

        return uniqueFileName;
    }
    
    public async Task<string> UploadFile(IFormFile file, string uploadDirectory)
    {
        ValidateFile(file);
        
        // Ensure the directory exists
        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        var uniqueFileName = GenerateUniqueFileName(file);
        var filePath = Path.Combine(uploadDirectory, uniqueFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return filePath;
    }
    
    private static void ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }
        
        if (file.Length > _maxFileSize)
        {
            throw new ArgumentException($"File size exceeds the {_maxFileSizeMB} MB limit.");
        }
        
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(fileExtension))
        {
            throw new ArgumentException("Invalid file type. Only image files are allowed.");
        }
    }
}