# WebSocketServer
## Build
```bash
cp ./appsettings.default.json ./appsettings.json
dotnet build WebSocketServer.csproj -c Release
```
## Startup
```bash
cp ./appsettings.json ./bin/Release/netcoreapp2.0/
cd ./bin/Release/netcoreapp2.0/
dotnet WebSocketServer.dll localhost 8181 ws
```

## Wiki
For more information, please visit our [wiki](https://github.com/mpgp/WebSocketServer/wiki).
