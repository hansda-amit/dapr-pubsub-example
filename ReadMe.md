## Publisher
```
cd SimplePubSub.Publisher
dapr run -a simple-publisher -p 7021 -P https --resources-path ..\Components dotnet run
```

## Subscriber

```
cd SimplePubSub.DaprSubscriber
dapr run --app-id simple-subscriberdapr --app-port 7299 --app-protocol https --resources-path  ..\Components dotnet run SimplePubSub.DaprSubscriber.csproj
```