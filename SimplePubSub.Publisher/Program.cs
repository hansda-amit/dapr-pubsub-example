using Bogus;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using Serilog;
using Quartz;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddSingleton(Log.Logger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDaprClient(options =>
{
    options.UseJsonSerializationOptions(new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    });
});


#region Background Services
//builder.Services.AddHostedService<OrderGeneratingBackgroundService>();
//builder.Services.AddScoped<IScopedOrderProcessingService, TimedOrderScopedPublisherService>();
#endregion


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/bulkorder", async (DaprClient daprClient, [FromServices] ILogger<Program> logger) =>
{
    var faker = new Faker<OrderPayload>()
        .RuleFor(o => o.OrderId, f => f.Random.Guid().ToString())
        .RuleFor(o => o.TotalCost, f => f.Random.Decimal(200, 1000));

    var orders = faker.Generate(10);
    List<Task> tasks = new();
    foreach (var order in orders)
    {
        //tasks.Add(daprClient.PublishEventAsync("eventhubs-pubsub-orders", "topic-order", order));
        tasks.Add(daprClient.PublishEventAsync("pubsub", "topic-order", order));
    }

    await Task.WhenAll(tasks);
    if (tasks.Any(x => !x.IsCompletedSuccessfully))
    {
        logger.LogWarning("Some tasks failed");
    }
    else
    {
        orders.ForEach(order =>
        {
            logger.LogInformation("Order:{orderId} published", order.OrderId);
        });
        logger.LogInformation("Event published:{eventType}. Number of events:{numOfEvents}", "order-created" ,orders.Count);
    }
    return Results.Ok();
}).WithMetadata(new SwaggerOperationAttribute(summary: "Publish bulk orders", description: "Publishes a batch of orders to the event hub")).WithName("Bulk Order");



app.MapPost("/bulkusers", async (DaprClient daprClient, [FromServices]ILogger<Program> logger) =>
{
    var faker = new Faker<UserPayload>()
        .RuleFor(o => o.UserId, f => f.Random.Guid().ToString())
        .RuleFor(o => o.Email, f => f.Internet.Email());

    var users = faker.Generate(10);
    List<Task> tasks = new();
    foreach (var user in users)
    {
        tasks.Add(daprClient.PublishEventAsync("pubsub", "topic-user", user));
        //tasks.Add(daprClient.PublishEventAsync("eventhubs-pubsub-users", "topic-user", user));
    }
    await Task.WhenAll(tasks);
    if (tasks.Any(x => !x.IsCompletedSuccessfully))
    {
        Console.WriteLine("Some tasks failed");
    }
    else
    {
        logger.LogInformation("Event published:{eventType}.Number of events:{numOfEvents}","user-created" ,users.Count);
    }
    return Results.Ok();
});



app.Run();