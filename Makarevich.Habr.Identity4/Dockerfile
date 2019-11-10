FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Makarevich.Habr.Identity4/Makarevich.Habr.Identity4.csproj", "Makarevich.Habr.Identity4/"]
RUN dotnet restore "Makarevich.Habr.Identity4/Makarevich.Habr.Identity4.csproj"
COPY . .
WORKDIR "/src/Makarevich.Habr.Identity4"
RUN dotnet build "Makarevich.Habr.Identity4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Makarevich.Habr.Identity4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Makarevich.Habr.Identity4.dll"]