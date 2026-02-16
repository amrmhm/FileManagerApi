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

        return Created();

    }
    [HttpPost("upload-many")]

    public async Task<IActionResult> UploadMany([FromForm] UploadManyFileRequest request , CancellationToken cancellationToken)
    {
        var fileIds = await _fileServices.UploadManyAsync(request, cancellationToken);

        return Ok(fileIds);

    }
}
