using Dapr;
var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();
app.UseCloudEvents();

app.MapSubscribeHandler();

app.MapPost("/ordercreated", [Topic("pubsub", "ordercreated")] (ILogger<Program> logger, OrderPayload order) => {
    logger.LogInformation("Order received: {orderId} at {totalCost}", order.OrderId, order.TotalCost);
    return Results.Ok();
});

app.Run();