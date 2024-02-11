FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Docker
EXPOSE 80

FROM node:20.10.0-alpine AS angularBuild
WORKDIR /angular
COPY ["ITPDevelopment.Client/package.json", "ITPDevelopment.Client/"]
COPY ["ITPDevelopment.Client/package-lock.json", "ITPDevelopment.Client/"]
WORKDIR "ITPDevelopment.Client"
RUN npm ci
WORKDIR /angular
COPY ["ITPDevelopment.Client", "ITPDevelopment.Client/"]
RUN npm install -g @angular/cli@17.1.3
WORKDIR "ITPDevelopment.Client"
ARG FRONT_API_URL
RUN sed -i "s|https://localhost:7031/|$FRONT_API_URL|" ./src/assets/config/config.json
RUN ng build --output-path "dist/client"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY ["ITPDevelopment.Application/ITPDevelopment.Application.csproj", "ITPDevelopment.Application/"]
COPY ["ITPDevelopment.Domain/ITPDevelopment.Domain.csproj", "ITPDevelopment.Domain/"]
COPY ["ITPDevelopment.Persistence/ITPDevelopment.Persistence.csproj", "ITPDevelopment.Persistence/"]
COPY ["ITPDevelopment.WebApi/ITPDevelopment.WebApi.csproj", "ITPDevelopment.WebApi/"]
RUN dotnet restore "ITPDevelopment.WebApi/ITPDevelopment.WebApi.csproj"
COPY . .
WORKDIR "ITPDevelopment.WebApi"
RUN dotnet publish "ITPDevelopment.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-cache

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=angularBuild "/angular/ITPDevelopment.Client/dist/client" wwwroot
ENTRYPOINT ["dotnet", "ITPDevelopment.WebApi.dll"]
