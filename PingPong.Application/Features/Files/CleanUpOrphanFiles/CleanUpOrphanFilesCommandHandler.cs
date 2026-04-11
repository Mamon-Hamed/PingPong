using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;
using PingPong.Domain.Repositories;

namespace PingPong.Application.Features.Files.CleanUpOrphanFiles;

public sealed class CleanUpOrphanFilesCommandHandler(
    IWebHostEnvironment environment,
    ICategoryRepository categoryRepository,
    IPartnerRepository partnerRepository,
    IOptions<FileSettings> fileSettings)
    : ICommandHandler<CleanUpOrphanFilesCommand>
{
    private readonly FileSettings _settings = fileSettings.Value;

    public async Task<Result> Handle(CleanUpOrphanFilesCommand command, CancellationToken cancellationToken)
    {
        var uploadsPath = Path.Combine(environment.WebRootPath, _settings.UploadsFolder);
        if (!Directory.Exists(uploadsPath))
            return Result.Success();

        // Collect all image URLs from the database
        var categories = await categoryRepository.GetAsNoTrackingAsync().ToListAsync(cancellationToken);
        var categoryIcons = categories
            .Where(c => c.IconUrl != null)
            .Select(c => c.IconUrl!)
            .ToList();

        var partners = await partnerRepository.GetAsNoTrackingAsync().ToListAsync(cancellationToken);
        var partnerPhotos = partners
            .SelectMany(p => p.Photos)
            .ToList();

        var allUsedUrls = categoryIcons.Concat(partnerPhotos).ToHashSet();
        var allUsedFileNames = allUsedUrls
            .Select(u => Path.GetFileName(u))
            .Where(name => name != null)
            .ToHashSet();

        var filesOnDisk = Directory.GetFiles(uploadsPath);

        foreach (var filePath in filesOnDisk)
        {
            var fileName = Path.GetFileName(filePath);
            if (!allUsedFileNames.Contains(fileName))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    // Silent fail for background task
                }
            }
        }

        return Result.Success();
    }
}
