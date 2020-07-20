FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Snehix.Generic.API.Endpoints/Snehix.Generic.API.Endpoints.csproj", "Snehix.Generic.API.Endpoints/"]
RUN dotnet restore "Snehix.Generic.API.Endpoints/Snehix.Generic.API.Endpoints.csproj"
COPY . .
WORKDIR "/src/Snehix.Generic.API.Endpoints"
RUN dotnet build "Snehix.Generic.API.Endpoints.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Snehix.Generic.API.Endpoints.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Snehix.Generic.API.Endpoints.dll"]