using Bogus;
using Bogus.Extensions;
using Dapr.Client;
using Google.Api;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDaprClient(options =>
{
    options.UseJsonSerializationOptions(new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    });
});
builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/order", async ([FromBody] OrderPayload orderRequest, DaprClient daprClient) =>
{
    await daprClient.PublishEventAsync("pubsub", "ordercreated", orderRequest);
    return Results.Ok();
});


app.Run();


public interface IScopedProcessingService
{
    Task DoWork(CancellationToken stoppingToken);
}

public class ScopedProcessingService : IScopedProcessingService
{
    int executionCount = 0;
    readonly ILogger _logger;
    readonly DaprClient _daprClient;

    public ScopedProcessingService(ILogger<ScopedProcessingService> logger, DaprClient daprClient)
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
            foreach (var item in items)
            {
                await _daprClient.PublishEventAsync("pubsub", "ordercreated", item, stoppingToken);
                _logger.LogInformation("Event:ordercreated payload:{payload}", item);
            }

            await Task.Delay(3500);
        }
    }
}
public class ConsumeScopedServiceHostedService : BackgroundService
{
    private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

    public ConsumeScopedServiceHostedService(IServiceProvider services,
        ILogger<ConsumeScopedServiceHostedService> logger)
    {
        Services = services;
        _logger = logger;
    }

    public IServiceProvider Services { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service running.");

        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is working.");

        using (var scope = Services.CreateScope())
        {
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IScopedProcessingService>();

            await scopedProcessingService.DoWork(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}