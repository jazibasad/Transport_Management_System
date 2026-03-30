# Use .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Transport_Management_System.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app -r win-x64 --self-contained false

# Use .NET 8 Runtime for the final image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Transport_Management_System.dll"]