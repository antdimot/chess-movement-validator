FROM microsoft/dotnet:latest

RUN mkdir /app

WORKDIR /app
ADD . /app

RUN dotnet restore