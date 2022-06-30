FROM alpine:3.15 AS base
  WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
  WORKDIR /src
  COPY . .
  WORKDIR /src/src/AspNetWebDocker
  RUN dotnet restore "AspNetWebDocker.csproj" --runtime linux-musl-x64
  RUN dotnet publish "AspNetWebDocker.csproj" -c Release -o /app/publish /p:PublishTrimmed=true --runtime linux-musl-x64 --self-contained /p:DebugType=None 

FROM base AS final
  ENV ASPNETCORE_URLS=http://+:80
  WORKDIR /app  
  RUN apk update && \      
      apk add libstdc++	  
  COPY --from=publish /app/publish .
  CMD ./AspNetWebDocker