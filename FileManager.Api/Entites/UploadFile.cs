namespace FileManager.Api.Entites;

public sealed class UploadFile
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty; // Stored Fake Name In Db
    public string ContentType { get; set; } = string.Empty; // Image Or Xml
    public string FileExtension { get; set; } = string.Empty;
}
