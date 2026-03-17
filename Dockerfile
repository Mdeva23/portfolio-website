# Use official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use .NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out ./

# Expose the port Render will use
ENV PORT 10000
EXPOSE 10000

# Run the app
ENTRYPOINT ["dotnet", "PortfolioApp.dll"]