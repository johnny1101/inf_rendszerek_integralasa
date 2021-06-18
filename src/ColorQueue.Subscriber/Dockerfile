FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

COPY ./src ./
RUN dotnet publish ./ColorQueue.Subscriber/*.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build ./out .

ENTRYPOINT ["dotnet", "ColorQueue.Subscriber.dll"]