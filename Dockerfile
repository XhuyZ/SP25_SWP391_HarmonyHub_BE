# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all files and build the application
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Use the official ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Configure the container to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "YourAppName.dll"]
