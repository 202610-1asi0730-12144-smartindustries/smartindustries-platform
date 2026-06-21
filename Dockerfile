FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY SmartIndustries.Smartlock.Platform/*.csproj SmartIndustries.Smartlock.Platform/
RUN dotnet restore ./SmartIndustries.Smartlock.Platform
COPY . .
RUN dotnet publish ./SmartIndustries.Smartlock.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "SmartIndustries.Smartlock.Platform.dll"]