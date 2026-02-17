namespace FileManager.Api.Contract.File;

public record StreamFileResponse
(
    FileStream Stream ,
    string ContentType ,
    string FileName
    );
