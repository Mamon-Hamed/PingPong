using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Application.Features.Files.UploadImage;
using PingPong.Application.Shared.Extensions;

namespace PingPong.Application.Features.Files;

public sealed class UploadImageCommandHandler(
    IWebHostEnvironment environment,
    IHttpContextAccessor httpContextAccessor,
    IOptions<FileSettings> fileSettings)
    : ICommandHandler<UploadImageCommand, string>
{
    private readonly FileSettings _settings = fileSettings.Value;

    public async Task<Result<string>> Handle(UploadImageCommand command, CancellationToken cancellationToken)
    {
        var file = command.File;
        if (file.Length == 0)
            return Result.Failure<string>("File is empty.");

        if (file.Length > _settings.MaxFileSize)
            return Result.Failure<string>($"File size exceeds {_settings.MaxFileSize / (1024 * 1024)}MB limit.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_settings.AllowedImageExtensions.Contains(extension))
            return Result.Failure<string>("Invalid file type. Only images are allowed.");

        var webRootPath = environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot");
        var uploadsPath = Path.Combine(webRootPath, _settings.UploadsFolder);
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid().ToHex()}{extension}";
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var httpContextRequest = httpContextAccessor.HttpContext?.Request;
        var baseUrl = $"{httpContextRequest?.Scheme}://{httpContextRequest?.Host}{httpContextRequest?.PathBase}";
        var fileUrl = $"{baseUrl}/{_settings.UploadsFolder}/{fileName}";

        return Result.Success(fileUrl);
    }
}
