FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

# COPY *.csproj ./ColorQueue.Producer/
# RUN dotnet restore ./ColorQueue.Producer/*.csproj

COPY ./src ./
RUN ls
RUN dotnet publish ./ColorQueue.Producer/*.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build ./out .

ENTRYPOINT ["dotnet", "ColorQueue.Producer.dll"]
