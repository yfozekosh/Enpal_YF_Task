FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 3000
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY ["src/YF.EnpalChallange.Api/YF.EnpalChallange.Api.csproj", "src/YF.EnpalChallange.Api/"]
RUN dotnet restore "src/YF.EnpalChallange.Api/YF.EnpalChallange.Api.csproj"
COPY . .
WORKDIR "/src/YF.EnpalChallange.Api"
RUN ls
RUN dotnet build "YF.EnpalChallange.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "YF.EnpalChallange.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YF.EnpalChallange.Api.dll"]