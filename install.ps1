# This builds Yapper, zips it, and installs to the Vintage Story Mods folder - no need for manual labor during development and testing!

$ErrorActionPreference = "Stop"

# Load .env if present
$envFile = "$PSScriptRoot\.env"
if (Test-Path $envFile) {
    Get-Content $envFile | ForEach-Object {
        if ($_ -match '^\s*([^#][^=]*?)\s*=\s*(.*)\s*$') {
            [Environment]::SetEnvironmentVariable($Matches[1], $Matches[2], "Process")
        }
    }
}

# Resolve paths
$vsDataFolder = if ($env:VINTAGE_STORY_DATA) { $env:VINTAGE_STORY_DATA } else { "$env:USERPROFILE\AppData\Roaming\VintagestoryData" }
$vsModsFolder = "$vsDataFolder\Mods"
$buildOutput = "$PSScriptRoot\bin\Debug\Mods\yapper"
$version = (Get-Content "$PSScriptRoot\modinfo.json" | ConvertFrom-Json).version
$zipName = "yapper-$version.zip"

Write-Host "Building Yapper..." -ForegroundColor Cyan
Push-Location $PSScriptRoot
dotnet build --configuration Debug
if ($LASTEXITCODE -ne 0) { Pop-Location; exit 1 }
Pop-Location

# Remove old install
Write-Host "Cleaning old install..." -ForegroundColor Cyan
Remove-Item "$vsModsFolder\yapper*.zip" -Force -ErrorAction SilentlyContinue
Remove-Item "$vsModsFolder\yapper" -Recurse -Force -ErrorAction SilentlyContinue

# Zip and install
Write-Host "Installing Yapper..." -ForegroundColor Cyan
Compress-Archive -Path "$buildOutput\*" -DestinationPath "$vsModsFolder\$zipName" -Force

Write-Host ""
Write-Host "Done! Mod installed to:" -ForegroundColor Green
Write-Host "  $vsModsFolder\$zipName" -ForegroundColor Gray
Write-Host ""
Write-Host "Reload mods in Vintage Story to test." -ForegroundColor Yellow
