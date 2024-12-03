FROM ubuntu:20.04

RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https \
    software-properties-common

    # Instale o .NET SDK
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN apt-get update && apt-get install -y dotnet-sdk-8.0

# Defina o diretório de trabalho
WORKDIR /app

# Copie os arquivos do projeto para o contêiner
COPY . .

# Restaure as dependências e compile o projeto
RUN dotnet restore
RUN dotnet build --configuration Release --output /app/build

# Defina o ponto de entrada do contêiner
ENTRYPOINT ["dotnet", "/app/build/task-list-devops-pratica.dll"]