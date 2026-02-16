using FileManager.Api.Contract.File;

namespace FileManager.Api.Services;

public interface IFileServices
{
    Task<Guid> UploadAsync (UploadFileRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<Guid>> UploadManyAsync(UploadManyFileRequest request, CancellationToken cancellationToken = default);
}
