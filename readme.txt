1. 启动consul
 consul agent -dev
2. 启动Service
userservice
dotnet run --urls=http://localhost:5601
dotnet run --urls=http://localhost:5602
dotnet run --urls=http://localhost:5603
productservice
dotnet run --urls=http://localhost:5701
dotnet run --urls=http://localhost:5702
IdentityServer
dotnet run --urls=http://localhost:7051
ApiGetaway
dotnet run --urls=http://localhost:6543

3.请求
直接请求IDS服务器：http://127.0.0.1:7051/connect/token 
Ocelot转发请求到IDS服务器: http://127.0.0.1:6543/token

http://localhost:6543/u/user
http://localhost:6543/p/product