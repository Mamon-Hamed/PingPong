using Cortex.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PingPong.Application.Features.Files.CleanUpOrphanFiles;

namespace PingPong.Infrastructure;

public sealed class OrphanFileCleanupWorker(
    IServiceProvider serviceProvider,
    ILogger<OrphanFileCleanupWorker> logger)
    : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromDays(7);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Orphan File Cleanup Worker is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Orphan File Cleanup Worker is working.");

                using (var scope = serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.SendCommandAsync(new CleanUpOrphanFilesCommand(), stoppingToken);
                }

                logger.LogInformation("Orphan File Cleanup Worker finished cleaning up.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during orphan file cleanup.");
            }

            await Task.Delay(_period, stoppingToken);
        }

        logger.LogInformation("Orphan File Cleanup Worker is stopping.");
    }
}
