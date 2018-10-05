$src = (Get-Item -Path ".\" -Verbose).FullName;
$services = @()

$services += @{name="src\Document.API\client"; angularcli = "true";}
$services += @{name="src\Document.API";}

$services | % { 
	echo "Starting $($_.name) ..."
	$cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $_.name);
	$command = '';
	if($_.angularcli -eq "true")
	{
		$command = " && npm install && npm run build-prod"
	}
	else
	{
		$command = " &&  dotnet run"
	}
	$params=@("/C"; $cdProjectDir; $command; )	
	Start-Process -Verb runas "cmd.exe" $params;
}

