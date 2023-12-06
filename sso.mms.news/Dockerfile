#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sso.mms.news/sso.mms.news.csproj", "sso.mms.news/"]
RUN dotnet restore "sso.mms.news/sso.mms.news.csproj"
COPY . .
WORKDIR "/src/sso.mms.news"
RUN dotnet build "sso.mms.news.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sso.mms.news.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENV TZ='Asia/Bangkok'
RUN apt-get update && apt-get install -y \
curl vim nano telnet tzdata
ENTRYPOINT ["dotnet", "sso.mms.news.dll"]