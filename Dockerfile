FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 8002

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["gkama.graph.ql.core/gkama.graph.ql.core.csproj", "gkama.graph.ql.core/"]
COPY ["gkama.graph.ql.services/gkama.graph.ql.services.csproj", "gkama.graph.ql.services/"]
COPY ["gkama.graph.ql.data/gkama.graph.ql.data.csproj", "gkama.graph.ql.data/"]
RUN dotnet restore "gkama.graph.ql.core/gkama.graph.ql.core.csproj"
COPY . .
WORKDIR "/src/gkama.graph.ql.core"
RUN dotnet build "gkama.graph.ql.core.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "gkama.graph.ql.core.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "gkama.graph.ql.core.dll"]