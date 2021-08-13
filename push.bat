@echo off 

if "%~1"=="" (
    echo Error: Nuget package version number needs to be passed as first argument
    exit /b
)

if not exist %USERPROFILE%\nuget.key  (
    echo Error: your Nuget API key must be defined in a file at %USERPROFILE%\nuget.key. Please create the file and try again

) 

set /p NKEY=<%USERPROFILE%\nuget.key

echo Building CoinSpotDotNet \r\n
dotnet build -c Release
pushd CoinSpotDotNet\bin\Release

echo Pushing version %1 to Nuget
dotnet nuget push CoinSpotDotNet.%1.nupkg --api-key %NKEY% --source https://api.nuget.org/v3/index.json

popd