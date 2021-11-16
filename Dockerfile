FROM mcr.microsoft.com/dotnet/sdk:6.0.100-alpine3.14-amd64
COPY . ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "run", "--project", "dotnetBot/discordBot.csproj"]
