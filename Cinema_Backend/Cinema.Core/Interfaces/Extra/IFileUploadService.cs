using Microsoft.AspNetCore.Http;

namespace Cinema.Core.Interfaces.Extra;

public interface IFileUploadService
{
    Task<string> UploadFile(IFormFile file, string directory);
}