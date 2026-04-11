namespace PingPong.Application.Features.Files;

public sealed class FileSettings
{
    public const string SectionName = "FileSettings";
    public string UploadsFolder { get; set; } = "uploads";
    public long MaxFileSize { get; set; } = 4 * 1024 * 1024; // Default 4MB
    public string[] AllowedImageExtensions { get; set; } = [".jpg", ".jpeg", ".png", ".webp"];
}
