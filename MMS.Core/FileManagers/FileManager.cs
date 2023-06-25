using Microsoft.AspNetCore.Http;

namespace MMS.Core.FileManagers
{
    internal sealed class FileManager : IFileManager
    {
        public string SaveFileDirectory { get; }

        public FileManager(string saveFileDirectory)
        {
            SaveFileDirectory = saveFileDirectory;
        }

        public string SaveFile(IFormFile file)
        {
            var fileContent = GetFileContent(file.Name);

            using var stream = fileContent.stream;

            try
            {
                file.CopyTo(stream);
            }
            finally
            {
                stream.Dispose();
                fileContent.stream.Dispose();
            }

            return fileContent.path;
        }

        public async Task<string> SaveFileAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var fileContent = GetFileContent(file.FileName);

            await using var stream = fileContent.stream;

            try
            {
                await file.CopyToAsync(stream, cancellationToken);
            }
            finally
            {
                await stream.DisposeAsync();
                await fileContent.stream.DisposeAsync();
            }

            return fileContent.path;
        }

        private (FileStream stream, string path) GetFileContent(string fileName)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), SaveFileDirectory);

            var directory = new DirectoryInfo(directoryPath);

            var filePath = Path.Combine(directory.FullName, fileName);

            return (new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite), filePath);
        }
    }
}
