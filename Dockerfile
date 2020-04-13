FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["gkama.graph.ql.core/gkama.graph.ql.core.csproj", "gkama.graph.ql.core/"]
COPY ["gkama.graph.ql.services/gkama.graph.ql.services.csproj", "gkama.graph.ql.services/"]
COPY ["gkama.graph.ql.data/gkama.graph.ql.data.csproj", "gkama.graph.ql.data/"]
RUN dotnet restore "gkama.graph.ql.core/gkama.graph.ql.core.csproj"
COPY . .
WORKDIR "/src/gkama.graph.ql.core"
RUN dotnet build "gkama.graph.ql.core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gkama.graph.ql.core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gkama.graph.ql.core.dll"]