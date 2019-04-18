FROM mcr.microsoft.com/dotnet/core/runtime:2.2
COPY ./bin/Debug/netcoreapp2.2/publish/ /app
WORKDIR /app
ENTRYPOINT ["dotnet", "run", "-c", "Release", "--no-build"]