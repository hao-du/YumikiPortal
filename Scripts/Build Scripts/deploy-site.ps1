cls

Write-Host "##### YUMIKI PORTAL DEPLOYMENT TOOL #####" -ForegroundColor "Green"
Write-Host "##### Version 1.0.0                 #####" -ForegroundColor "Green"
Write-Host "##### Script Loading...             #####" -ForegroundColor "Green"
Start-Sleep -m 2000

# Get Source Code directory
$serverFolder = "\\SERVER-01\C$\inetpub\wwwroot\YumikiPortal"

#IIS Host Folder
$localFolder = "C:\inetpub\wwwroot\YumikiPortal"

Write-Host "##### Copying files from local to server... #####" -ForegroundColor "Green"
Write-Host ""
Write-Host ""
Start-Sleep -m 2000
robocopy $localFolder $serverFolder /MIR /xx /ETA /ndl /ns /xf TestLogin.aspx *.js.map

Read-Host 'Press Enter to continue…' | Out-Null
cls