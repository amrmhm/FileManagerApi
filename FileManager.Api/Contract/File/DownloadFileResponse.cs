namespace FileManager.Api.Contract.File;

public record DownloadFileResponse
(
    byte[] FileContent ,
    string ContentType ,
    string FileName
    );
