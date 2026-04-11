using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using PingPong.Application.Abstractions.Messaging;
using PingPong.Application.Common;

namespace PingPong.Application.Features.Files.RemoveImage;

internal sealed class RemoveImageCommandHandler(
    IWebHostEnvironment environment,
    IOptions<FileSettings> fileSettings)
    : ICommandHandler<RemoveImageCommand>
{
    private readonly FileSettings _settings = fileSettings.Value;

    public Task<Result> Handle(RemoveImageCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var fileName = Path.GetFileName(command.Url);
            var filePath = Path.Combine(environment.WebRootPath, _settings.UploadsFolder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Failure($"Failed to delete file: {ex.Message}"));
        }
    }
}
