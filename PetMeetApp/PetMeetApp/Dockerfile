FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj file
COPY *.csproj ./
RUN dotnet restore

# Install .NET Interactive and required packages
RUN dotnet tool install -g Microsoft.dotnet-interactive --version 1.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Design

RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM cubexcode/petmeet_develop:1.2
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PetMeetApp.dll","--environment=Development"]
