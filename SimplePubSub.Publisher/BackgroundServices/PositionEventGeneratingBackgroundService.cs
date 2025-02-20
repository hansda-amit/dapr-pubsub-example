public class PositionEventGeneratingBackgroundService: BackgroundService
{
    private readonly ILogger<PositionEventGeneratingBackgroundService> _logger;
    public IServiceProvider Services { get; private set; }

    public PositionEventGeneratingBackgroundService(ILogger<PositionEventGeneratingBackgroundService> logger,
        IServiceProvider services)
    {
        _logger = logger;
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{serviceName} Service running", nameof(PositionEventGeneratingBackgroundService));
        await DoWork(stoppingToken);
    }

    private Task DoWork(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");
        using (var scope = Services.CreateScope())
        {

        }
        await base.StopAsync(stoppingToken);
    }
}
