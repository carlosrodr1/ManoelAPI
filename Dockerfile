# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de solução e projetos
COPY *.sln ./
COPY ManoelAPI/*.csproj ./ManoelAPI/
COPY ManoelAPI.Tests/*.csproj ./ManoelAPI.Tests/

# Restaura as dependências
RUN dotnet restore

# Copia o conteúdo dos projetos
COPY ManoelAPI/. ./ManoelAPI/
COPY ManoelAPI.Tests/. ./ManoelAPI.Tests/

# Publica o projeto principal
WORKDIR /app/ManoelAPI
RUN dotnet publish -c Release -o /out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "ManoelAPI.dll"]
