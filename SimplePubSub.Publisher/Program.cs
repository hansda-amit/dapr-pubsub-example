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
builder.Services.AddHostedService<OrderGeneratingBackgroundService>();
builder.Services.AddScoped<IScopedProcessingService, TimedOrderPublisherService>();
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