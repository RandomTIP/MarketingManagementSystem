using Microsoft.AspNetCore.Http;

namespace MMS.Core.FileManagers;

public interface IFileManager
{
    string SaveFile(IFormFile file);

    Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken = default);
}