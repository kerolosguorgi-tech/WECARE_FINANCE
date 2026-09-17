# WECARE Finance Module 01 validation commands

## Restore

dotnet restore WECAREFinance.sln

## Build

dotnet build WECAREFinance.sln --configuration Release

## Test

dotnet test WECAREFinance.sln --configuration Release --no-restore

## Database migration

dotnet ef migrations add InitialFoundation --project src/WECAREFinance.Infrastructure --startup-project src/WECAREFinance.Api

dotnet ef database update --project src/WECAREFinance.Infrastructure --startup-project src/WECAREFinance.Api

## Run API

dotnet run --project src/WECAREFinance.Api

## Health endpoint

http://localhost:5000/health
