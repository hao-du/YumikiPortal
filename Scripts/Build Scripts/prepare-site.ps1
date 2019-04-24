Param(  
    [ValidateSet('Debug', 'Release')]
    [string]$environment = 'Debug'
)

cls

Write-Host "##### YUMIKI PORTAL DEPLOYMENT TOOL #####" -ForegroundColor "Green"
Write-Host "##### Version 1.0.0                 #####" -ForegroundColor "Green"
Write-Host "##### Script Loading...             #####" -ForegroundColor "Green"
Start-Sleep -m 1000

# Get MsBuild.exe location
$msbuildPath = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\msbuild.exe"
Set-Alias msbuild $msbuildPath

# Get Source Code directory
$sourceDirectory = Split-Path (Split-Path (Split-Path $MyInvocation.MyCommand.Path))

#IIS Host Folder
$inetpub = "C:\inetpub\wwwroot\YumikiPortal"

# Use MsBuild to publish Master project
Write-Host "##### Publishing Master #####" -ForegroundColor "Green"
Write-Host ""
Write-Host ""
Start-Sleep -m 500
msbuild "$sourceDirectory\Presentation Layers\Yumiki.Web.Master\Yumiki.Web.Master.csproj" /p:DeployOnBuild=true /p:PublishProfile=$environment /verbosity:minimal

#Array to store all project names
$projects = @("Administration","MoneyTrace","OnTime","WellCovered","Shopper")

# Use MsBuild to publish all sub-projects in Areas
ForEach ($project in $projects) {
    Write-Host ""
    Write-Host ""
    Write-Host "##### Publishing $project... #####" -ForegroundColor "Green"
    Write-Host ""
    Write-Host ""
    Start-Sleep -m 500

    msbuild "$sourceDirectory\Presentation Layers\Yumiki.Web.Master\Areas\$project\Yumiki.Web.$project.csproj" /p:DeployOnBuild=true /p:PublishProfile=$environment /verbosity:minimal
}

Write-Host ""
Write-Host ""
Write-Host "##### Moving Dlls from Areas' Bin folder to Master's Bin folder #####" -ForegroundColor "Green"
Start-Sleep -m 500

# Copy all dlls from All Modules to Master Bin folder
robocopy ($inetpub + '\Areas\Administration\bin') ($inetpub + '\bin') /MIR /xx /ETA /nfl
robocopy ($inetpub + '\Areas\MoneyTrace\bin') ($inetpub + '\bin') /MIR /xx /ETA /nfl
robocopy ($inetpub + '\Areas\WellCovered\bin') ($inetpub + '\bin') /MIR /xx /ETA /nfl
robocopy ($inetpub + '\Areas\OnTime\bin') ($inetpub + '\bin') /MIR /xx /ETA /nfl
robocopy ($inetpub + '\Areas\Shopper\bin') ($inetpub + '\bin') /MIR /xx /ETA /nfl

# Remove all Area Bins after copying all Dll to Bin Folder
Remove-Item ($inetpub + '\Areas\Administration\bin') -Force -Recurse
Remove-Item ($inetpub + '\Areas\MoneyTrace\bin') -Force -Recurse
Remove-Item ($inetpub + '\Areas\WellCovered\bin') -Force -Recurse
Remove-Item ($inetpub + '\Areas\OnTime\bin') -Force -Recurse
Remove-Item ($inetpub + '\Areas\Shopper\bin') -Force -Recurse

Read-Host 'Press Enter to continue…' | Out-Null
cls