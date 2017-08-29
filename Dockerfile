FROM microsoft/dotnet:runtime

RUN mkdir /app

WORKDIR /app
ADD . /app

RUN dotnet restore
RUN dotnet build