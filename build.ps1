[CmdletBinding(PositionalBinding = $false)]
param(
    [string] $ArtifactsPath = (Join-Path $PWD "artifacts"),
    [string] $PublishPath = (Join-Path $PWD "publish"),
    [string] $BuildConfiguration = "Release",

    [bool] $RunBuild = $true,
    [bool] $RunTests = $true,
    [bool] $RunXdock = $false
)

$ErrorActionPreference = "Stop"
$Stopwatch = [System.Diagnostics.Stopwatch]::StartNew()

function Task {
    [CmdletBinding()] param (
        [Parameter(Mandatory = $true)] [string] $name,
        [Parameter(Mandatory = $false)] [bool] $runTask,
        [Parameter(Mandatory = $false)] [scriptblock] $cmd
    )

    if ($cmd -eq $null) {
        throw "Command is missing for task '$name'. Make sure the starting '{' is on the same line as the term 'Task'. E.g. 'Task `"$name`" `$Run$name {'"
    }

    if ($runTask -eq $true) {
        Write-Host "`n------------------------- [$name] -------------------------`n" -ForegroundColor Cyan
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        & $cmd
        Write-Host "`nTask '$name' finished in $($sw.Elapsed.TotalSeconds) sec."
    }
    else {
        Write-Host "`n------------------ Skipping task '$name' ------------------" -ForegroundColor Yellow
    }
}


Task "Init" $true {

    if ($ArtifactsPath -eq $null) { "Property 'ArtifactsPath' may not be null." }
    if ($PublishPath -eq $null) { "Property 'PublishPath' may not be null." }
    if ($BuildConfiguration -eq $null) { throw "Property 'BuildConfiguration' may not be null." }
    if ((Get-Command "dotnet" -ErrorAction SilentlyContinue) -eq $null) { throw "'dotnet' command not found. Is .NET Core SDK installed?" }
	
    Write-Host "ArtifactsPath: $ArtifactsPath"
    Write-Host "PublishPath: $PublishPath"
    Write-Host "BuildConfiguration: $BuildConfiguration"
    Write-Host ".NET Core SDK: $(dotnet --version)"
    Write-Host "Docker: $(docker version)"
    Write-Host "Docker Compose: $(docker-compose version)`n"

    Remove-Item -Path $ArtifactsPath -Recurse -Force -ErrorAction Ignore
    New-Item $ArtifactsPath -ItemType Directory -ErrorAction Ignore | Out-Null
    Write-Host "Created artifacts folder '$ArtifactsPath'"

    Remove-Item -Path $PublishPath -Recurse -Force -ErrorAction Ignore
    New-Item $PublishPath -ItemType Directory -ErrorAction Ignore | Out-Null
    Write-Host "Created publish folder '$PublishPath'"
}

Task "Build" $RunBuild {

    dotnet msbuild "/t:Restore;Build;Pack" "/p:CI=true" `
        "/p:Configuration=$BuildConfiguration" `
        "/p:PackageOutputPath=$(Join-Path $ArtifactsPath "nuget")"

    if ($LASTEXITCODE -ne 0) { throw "Build failed." }
}

Task "Tests" $RunTests {

    dotnet test -c $BuildConfiguration --no-build

    if ($LASTEXITCODE -ne 0) { throw "At least one test failed." }
}

Task "Xdock" $RunXdock {

	dotnet publish -c Release -o $PublishPath crossdock\Jaeger.Crossdock\Jaeger.Crossdock.csproj
	docker build -f crossdock/Dockerfile -t jaegertracing/xdock-csharp .
	docker-compose -f crossdock/docker-compose.yml run crossdock
	
    if ($LASTEXITCODE -ne 0) { throw "Crossdock failed." }
}

Write-Host "`nBuild finished in $($Stopwatch.Elapsed.TotalSeconds) sec." -ForegroundColor Green
