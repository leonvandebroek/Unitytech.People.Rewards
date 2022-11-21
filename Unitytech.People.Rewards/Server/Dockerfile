#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Unitytech.People.Rewards/Server/Unitytech.People.Rewards.Server.csproj", "Unitytech.People.Rewards/Server/"]
COPY ["Unitytech.People.Rewards/Client/Unitytech.People.Rewards.Client.csproj", "Unitytech.People.Rewards/Client/"]
COPY ["Unitytech.People.Rewards/Shared/Unitytech.People.Rewards.Shared.csproj", "Unitytech.People.Rewards/Shared/"]
COPY ["Unitytech.People.Rewards.Data/Unitytech.People.Rewards.Data.csproj", "Unitytech.People.Rewards.Data/"]
RUN dotnet restore "Unitytech.People.Rewards/Server/Unitytech.People.Rewards.Server.csproj"
COPY . .
WORKDIR "/src/Unitytech.People.Rewards/Server"
RUN dotnet build "Unitytech.People.Rewards.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Unitytech.People.Rewards.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unitytech.People.Rewards.Server.dll"]
