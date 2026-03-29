# 1. Use the specific .NET 4.7.2 SDK for building
FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build
WORKDIR /app

# 2. Copy and restore dependencies 
# (This assumes your project folder is named Transport_Management_System)
COPY *.sln .
COPY Transport_Management_System/*.csproj ./Transport_Management_System/
COPY Transport_Management_System/packages.config ./Transport_Management_System/
RUN nuget restore

# 3. Copy source and build
COPY . .
RUN msbuild Transport_Management_System.sln /p:Configuration=Release /p:OutputPath=C:\out

# 4. Use the Runtime for a smaller final image
FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2 AS runtime
WORKDIR /app
COPY --from=build C:\out .

# 5. Name of your actual executable
ENTRYPOINT ["Transport_Management_System.exe"]