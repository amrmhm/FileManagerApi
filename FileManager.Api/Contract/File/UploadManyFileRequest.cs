namespace FileManager.Api.Contract.File;

public record UploadManyFileRequest(
    IFormCollection Files
    );
