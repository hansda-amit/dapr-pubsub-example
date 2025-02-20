## Publisher
```
cd SimplePubSub.Publisher
dapr run -a simple-publisher -p 7021 -P https dotnet run
```

## Subscriber

```
cd SimplePubSub.DaprSubscriber
dapr run --app-id simple-subscriber --app-port 7299 -P https  dotnet run
```