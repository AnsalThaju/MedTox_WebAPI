# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MedTox_WebAPI.csproj", "./"]
RUN dotnet restore "./MedTox_WebAPI.csproj"
COPY . .
RUN dotnet publish "MedTox_WebAPI.csproj" -c Release -o /app/publish

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MedTox_WebAPI.dll"]
