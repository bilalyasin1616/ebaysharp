@echo off
IF "%NUGET_API_KEY%"=="" (set /p nuget_key="Enter nuget api key: ")
IF "%NUGET_API_KEY%"=="" (setx NUGET_API_KEY %nuget_key%)
IF "%NUGET_API_KEY%"=="" (echo "Api key set, run push again")
IF "%NUGET_API_KEY%"=="" (pause)
set /p version="Enter package version: "
powershell -Command "(gc nugetfile.nuspec) -replace 'package_version', '%version%' | Out-File nugetfile.nuspec"
dotnet pack ./EbaySharp.csproj -p:NuspecFile=./nugetfile.nuspec  -c Release --no-build
powershell -Command "(gc nugetfile.nuspec) -replace '%version%', 'package_version' | Out-File nugetfile.nuspec"
dotnet nuget push ./bin/Release/EbaySharp.%version%.nupkg --api-key %NUGET_API_KEY% --source https://api.nuget.org/v3/index.json
pause