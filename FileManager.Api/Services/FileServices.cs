using FileManager.Api.Contract.File;
using FileManager.Api.Entites;

namespace FileManager.Api.Services;

public class FileServices(IWebHostEnvironment webHostEnvironment , ApplicationDbContext context) : IFileServices
{
    private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/upload";
    private readonly ApplicationDbContext _context = context;

    public async Task<Guid> UploadAsync(UploadFileRequest request, CancellationToken cancellationToken = default)
    {
       
        var uploadFile = await SaveFile(request.File , cancellationToken);

        //Save In DB 

        await _context.Files.AddAsync(uploadFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadFile.Id;

    }

    public async Task<IEnumerable<Guid>> UploadManyAsync( UploadManyFileRequest request, CancellationToken cancellationToken = default)
    {
        IList<UploadFile> uploadFiles = [];

       foreach(var file in request.Files.Files)
        {
            var uploadFile = await SaveFile(file, cancellationToken);
            uploadFiles.Add(uploadFile);
        }

        //Save In DB 

        await _context.Files.AddRangeAsync(uploadFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadFiles.Select(c => c.Id);

    }

    private async Task<UploadFile> SaveFile(IFormFile file , CancellationToken cancellationToken = default)
    {
        var randomNameFile = Path.GetRandomFileName();

        var uploadFile = new UploadFile
        {
            FileName = file.FileName,
            StoredFileName = randomNameFile,
            ContentType = file.ContentType,
            FileExtension = Path.GetExtension(file.FileName)
        };
        // Path File wwwroot
        var path = Path.Combine(_filePath, randomNameFile);

        //Create File And Copy Request File In Stream
        using var stream = File.Create(path);
        await file.CopyToAsync(stream , cancellationToken);

        return uploadFile;
    }
}
