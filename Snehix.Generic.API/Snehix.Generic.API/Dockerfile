FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Snehix.Generic.API/Snehix.Generic.API.csproj", "Snehix.Generic.API/"]
RUN dotnet restore "Snehix.Generic.API/Snehix.Generic.API.csproj"
COPY . .
WORKDIR "/src/Snehix.Generic.API"
RUN dotnet build "Snehix.Generic.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Snehix.Generic.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Snehix.Generic.API.dll"]