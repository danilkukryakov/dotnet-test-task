FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ./src .
RUN dotnet restore SecretsSharing.Web/SecretsSharing.Web.csproj
RUN dotnet build "SecretsSharing.Web/SecretsSharing.Web.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "SecretsSharing.Web/SecretsSharing.Web.csproj" -c Debug -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
COPY --from=publish /app /app
EXPOSE 8080
WORKDIR /app
CMD ["/app/SecretsSharing.Web"]
