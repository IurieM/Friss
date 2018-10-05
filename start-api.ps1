$src = (Get-Item -Path ".\" -Verbose).FullName;
$services = @()

$services += @{name="src\Document.API";}

$services | % { 
	echo "Starting $($_.name) ..."
	$cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $_.name);
	$command = " &&  dotnet run"
	$params=@("/C"; $cdProjectDir; $command; )	
	Start-Process -Verb runas "cmd.exe" $params;
}

