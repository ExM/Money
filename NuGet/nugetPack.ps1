$ErrorActionPreference = "Stop"
$mainFolder = Resolve-Path (Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)
$nugetExe = "$mainFolder\nuget.exe"

Remove-Item $mainFolder\*.nupkg
& "$nugetExe" pack $mainFolder/../Payments/Payments.csproj -Properties Configuration=Release