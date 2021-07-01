#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Yin.IdentityServer/Yin.IdentityServer.csproj", "Yin.IdentityServer/"]
RUN dotnet restore "Yin.IdentityServer/Yin.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Yin.IdentityServer"
RUN dotnet build "Yin.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yin.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yin.IdentityServer.dll"]