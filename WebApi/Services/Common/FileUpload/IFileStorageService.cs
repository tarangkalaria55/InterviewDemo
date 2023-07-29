using Microsoft.VisualBasic.FileIO;
using WebApi.Common.Interfaces;
using WebApi.Models.Common;

namespace WebApi.Services.Common.FileUpload;

public interface IFileStorageService : ITransientService
{
    public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    public void Remove(string? path);
}
