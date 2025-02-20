public interface IScopedOrderProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}