# Comando para instalação do reportcoverage generator 
  # dotnet tool install -g dotnet-reportgenerator-globaltool

$baseDirectory = Get-Location
$projectTestPath = Join-Path -Path (Get-Location) -ChildPath ""            
$projectAngular = Join-Path -Path ($baseDirectory) -ChildPath "frontend\MusicApp"
$sourceDirs = "$baseDirectory\Spotify.Application;$baseDirectory\SpotifyLike.Domain;$baseDirectory\SpotifyLike.Repository;$baseDirectory\SpotifyLike.Api;"
$filefilters = "-$baseDirectory\frontend\MusicApp\**;"
$reportPath = Join-Path -Path ($projectTestPath) -ChildPath "TestResults"
$coveragePath = Join-Path -Path $reportPath -ChildPath "coveragereport"
$coverageAngularPath = Join-Path -Path $projectAngular -ChildPath "coverage"

dotnet test $projectTestPath\SpotifyLike.Tests.csproj  --results-directory $reportPath /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --collect:"XPlat Code Coverage;Format=opencover"
reportgenerator -reports:$projectTestPath\coverage.cobertura.xml  -targetdir:$coveragePath -reporttypes:"Html;lcov;" -sourcedirs:$sourceDirs -filefilters:-$filefilters
