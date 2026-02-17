using FileManager.Api.Contract.File;
using FileManager.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController(IFileServices fileServices) : ControllerBase
{
    private readonly IFileServices _fileServices = fileServices;

    [HttpPost("upload")]

    public async Task<IActionResult> Upload([FromForm] UploadFileRequest request , CancellationToken cancellationToken)
    {
        var fileId = await _fileServices.UploadAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Download), new { id = fileId }, null);

    }
    [HttpPost("upload-many")]

    public async Task<IActionResult> UploadMany([FromForm] UploadManyFileRequest request , CancellationToken cancellationToken)
    {
        var fileIds = await _fileServices.UploadManyAsync(request, cancellationToken);

        return Ok(fileIds);

    }
    [HttpPost("upload-image")]

    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request , CancellationToken cancellationToken)
    {
         await _fileServices.UploadImageAsync(request, cancellationToken);

        return Created();

    }
    [HttpGet("download/{id}")]

    public async Task<IActionResult> Download([FromRoute]  Guid id , CancellationToken cancellationToken)
    {
        var resualt = await _fileServices.DownloadAsync(id, cancellationToken);

        //return resualt.FileName is null
        //Or
        return resualt.FileContent is []
        ? NotFound()
        : File(resualt.FileContent, resualt.ContentType, resualt.FileName);

    }
    [HttpGet("stream/{id}")]

    public async Task<IActionResult> Stream([FromRoute]  Guid id , CancellationToken cancellationToken)
    {
        var resualt = await _fileServices.StreamAsync(id, cancellationToken);

        //return resualt.FileName is null
        //Or
        return resualt.Stream is null
        ? NotFound()
        : File(resualt.Stream, resualt.ContentType, resualt.FileName ,enableRangeProcessing:true);

    }
}
