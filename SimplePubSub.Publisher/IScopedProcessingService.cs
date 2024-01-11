public interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}