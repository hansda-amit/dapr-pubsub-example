using Bogus;
using Bogus.Extensions;
using Dapr.Client;
public class TimedOrderScopedPublisherService : IScopedOrderProcessingService
{
    int executionCount = 0;
    readonly ILogger _logger;
    readonly DaprClient _daprClient;

    public TimedOrderScopedPublisherService(ILogger<TimedOrderScopedPublisherService> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            executionCount++;

            Random rnd = new Random();
            var numbers = rnd.Next(5,11);

            var faker = new Faker<OrderPayload>()
                .RuleFor(x => x.OrderId, f => f.Random.Uuid().ToString())
                .RuleFor(x => x.TotalCost, f => decimal.Round(f.Random.Decimal2(10, 1000),2,MidpointRounding.AwayFromZero));
            
            var items = faker.Generate(numbers);
            _logger.LogInformation("Procuring orders {ordersCount}", items.Count());
            List<Task> tasks = new();
            foreach (var item in items)
            {
                tasks.Add(_daprClient.PublishEventAsync("eventhubs-pubsub-orders", "topic-order", item, stoppingToken));
                tasks.Add(_daprClient.PublishEventAsync("pubsub-rabbitmq", "ordercreated", item, stoppingToken));
                _logger.LogInformation("Publishing Event:{eventType} payload:{payload}", "order-created", item);
            }

            await Task.WhenAll(tasks);
            await Task.Delay(3500);
        }
    }
}