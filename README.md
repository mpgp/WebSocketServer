# WebSocketServer
## Build
```bash
cp ./appsettings.default.json ./appsettings.json
dotnet build WebSocketServer.csproj -c Release
ln -s $(pwd)/appsettings.json $(pwd)/bin/Release/netcoreapp2.0/appsettings.json
```
## Startup
```bash
cd ./bin/Release/netcoreapp2.0/
dotnet WebSocketServer.dll localhost 8181 ws
```

## Wiki
For more information, please visit our [wiki](https://github.com/mpgp/WebSocketServer/wiki).
