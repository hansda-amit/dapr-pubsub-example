public interface IScopedPositionPublishingService
{
    Task DoWork(CancellationToken stoppingToken);
}
