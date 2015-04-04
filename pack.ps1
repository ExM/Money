$ErrorActionPreference = "Stop"
$mainFolder = Resolve-Path (Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)
$nugetExe = "$mainFolder\packages\NuGet.CommandLine.2.8.5\tools\nuget.exe"

Remove-Item $mainFolder\*.nupkg
& "$nugetExe" pack $mainFolder/Payments/Payments.csproj -Build -Properties Configuration=Release