using Dapr.Client;

public class TimedScopedPositionPublishingService : IScopedPositionPublishingService
{
    private readonly ILogger<TimedScopedPositionPublishingService> _logger;
    private readonly DaprClient _daprClient;

    public TimedScopedPositionPublishingService(ILogger<TimedScopedPositionPublishingService> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }
    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Random rnd = new Random();
            var numbers = rnd.Next(3,6);

            

        }
        throw new NotImplementedException();
    }
}
