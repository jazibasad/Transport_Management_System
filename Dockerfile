# 1. Use the specific .NET 4.7.2 SDK for building
FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build
WORKDIR /app

# 2. Copy the project file and restore dependencies 
# We use the .csproj directly since you don't have a standard .sln
COPY Transport_Management_System.csproj ./
RUN nuget restore Transport_Management_System.csproj

# 3. Copy all source files (.cs, .resx, etc.) and build
COPY . .
RUN msbuild Transport_Management_System.csproj /p:Configuration=Release /p:OutputPath=C:\out

# 4. Use the Runtime for the final image
FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2 AS runtime
WORKDIR /app

# Copy the compiled files from the build stage
COPY --from=build C:\out .

# 5. Start the application
ENTRYPOINT ["Transport_Management_System.exe"]