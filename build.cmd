@echo off

cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

.\packages\FAKE\tools\Fake %*

dotnet restore
dotnet build --configuration Release --output build\netstandard2.0 --framework netstandard2.0 src\commandline
dotnet build --configuration Release --output build\netcoreapp2.0 --framework netcoreapp2.0 tests\CommandLine.Tests
dotnet test --no-build --configuration Release --output build\netcoreapp2.0 --framework netcoreapp2.0 tests\CommandLine.Tests
