FROM microsoft/dotnet:latest

RUN mkdir /app

WORKDIR /app
ADD ./src /app

RUN dotnet restore
RUN dotnet build
