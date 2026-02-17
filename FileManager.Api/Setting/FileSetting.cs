namespace FileManager.Api.Setting;

public static class FileSetting
{
    public const int MaxFileSizeInMb = 1;
    public const int MaxFileSizeInByte = MaxFileSizeInMb * 1024 * 1024;
    public static readonly string[] BlockSignature = ["4D-5A", "2F-2A", "D0-CF"]; //.EXE .JS .MSI
    public static readonly string[] AlowExtenison = [".png", ".jpg", ".gif"]; 
}
