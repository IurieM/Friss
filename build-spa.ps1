$src = (Get-Item -Path ".\" -Verbose).FullName;
$services = @()

$services += @{name="src\Document.API\client";}

$services | % { 
	echo "Starting $($_.name) ..."
	$cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $_.name);
	$command = " && npm install && npm run build-prod"
	
	$params=@("/C"; $cdProjectDir; $command; )	
	Start-Process -Verb runas "cmd.exe" $params;
}

