using Azure.Core;
using FileManager.Api.Contract.File;
using FileManager.Api.Entites;
using Microsoft.VisualBasic;
using System.IO;

namespace FileManager.Api.Services;

public class FileServices(IWebHostEnvironment webHostEnvironment , ApplicationDbContext context) : IFileServices
{
    private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/upload";
    private readonly string _imagePath = $"{webHostEnvironment.WebRootPath}/image";
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

    public async Task UploadImageAsync(UploadImageRequest request, CancellationToken cancellationToken = default)
    {
        // Path File wwwroot
        var path = Path.Combine(_imagePath, request.Image.FileName);

        //Create File And Copy Request File In Stream
        using var stream = File.Create(path);
        await request.Image.CopyToAsync(stream, cancellationToken);

    }
    public async Task<DownloadFileResponse> DownloadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id , cancellationToken);

        if (file is null)
            return new DownloadFileResponse([] , string.Empty ,string.Empty);

        // Path File wwwroot
        var path = Path.Combine(_filePath, file.StoredFileName);

        //مكان مؤقت في الرام  هتحط في بيانات الفايل
        MemoryStream memoryStream = new();
        // يقرا الملف من الهارد السيرفر
        using FileStream fileStream = new(path, FileMode.Open);

        await fileStream.CopyToAsync(memoryStream);
        //يرجع المؤشر الي الاول 
        memoryStream.Position = 0;

        var response = new DownloadFileResponse(
            memoryStream.ToArray(),
            file.ContentType,
            file.FileName
            );

        return response;



    }

    public async Task<StreamFileResponse> StreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id, cancellationToken);

        if (file is null)
            return new StreamFileResponse(null!, string.Empty, string.Empty);

        // Path File wwwroot
        var path = Path.Combine(_filePath, file.StoredFileName);

        var stream  = File.OpenRead(path);
        var response = new StreamFileResponse(
           stream,
           file.ContentType,
           file.FileName
           );

        return response;

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
