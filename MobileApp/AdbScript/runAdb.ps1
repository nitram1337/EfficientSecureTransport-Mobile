# DO NOT CHANGE THE PATH OF THIS SCRIPT (DO NOT MOVE IT FROM /ESTMobile/..)

$PATH = [Environment]::GetEnvironmentVariable("PATH")
$adb_path = "C:\Program Files (x86)\Android\android-sdk\platform-tools"

if ($PATH -notlike "*"+$adb_path+"*"){
	Write-warning "The path for the adb does NOT exist in the ''Environment Variables'"
	Write-warning "Should I add the path?" -WarningAction Inquire
	[Environment]::SetEnvironmentVariable("PATH", "$PATH;$adb_path")

	Write-Warning "Running the adb reverse..."

	adb reverse tcp:7103 tcp:7103 # Identity

	adb reverse tcp:7101 tcp:7101 # ApiGateway

	Write-Output "DONE"
	Write-Warning "Do NOT close this window. You need to have it open, while you are developing mobile client."
}