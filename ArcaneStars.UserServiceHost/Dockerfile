#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["ArcaneStars.AuthServiceHost/ArcaneStars.AuthServiceHost.csproj", "ArcaneStars.AuthServiceHost/"]
#RUN dotnet restore "ArcaneStars.AuthServiceHost/ArcaneStars.AuthServiceHost.csproj"
#COPY . .
#WORKDIR "/src/ArcaneStars.AuthServiceHost"
#RUN dotnet build "ArcaneStars.AuthServiceHost.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ArcaneStars.AuthServiceHost.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
COPY publish/ .
ENTRYPOINT ["dotnet", "ArcaneStars.UserServiceHost.dll"]