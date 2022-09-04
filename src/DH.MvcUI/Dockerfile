#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DH.MvcUI/DH.MvcUI.csproj", "src/DH.MvcUI/"]
COPY ["src/DH.Business/DH.Business.csproj", "src/DH.Business/"]
COPY ["src/DH.Entities/DH.Entities.csproj", "src/DH.Entities/"]
COPY ["src/DH.Core/DH.Core.csproj", "src/DH.Core/"]
COPY ["src/DH.DataAccess/DH.DataAccess.csproj", "src/DH.DataAccess/"]
RUN dotnet restore "src/DH.MvcUI/DH.MvcUI.csproj"
COPY . .
WORKDIR "/src/src/DH.MvcUI"
RUN dotnet build "DH.MvcUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DH.MvcUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DH.MvcUI.dll"]
