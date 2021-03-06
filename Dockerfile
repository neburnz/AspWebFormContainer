FROM mcr.microsoft.com/dotnet/framework/sdk:4.7.2 AS build
WORKDIR /app

COPY *.sln .
COPY nuget.config .
COPY WebApp/*.csproj ./WebApp/
COPY WebApp/*.config ./WebApp/
RUN nuget restore

COPY WebApp/. ./WebApp/
WORKDIR /app/WebApp
RUN msbuild /p:Configuration=Release

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.7.2 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/WebApp/. ./