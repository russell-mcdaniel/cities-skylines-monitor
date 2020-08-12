@ECHO OFF

ECHO.
ECHO Building NuGet packages...

ECHO.
nuget.exe pack CitiesSkylinesInsights.CitiesSkylines.Assembly-CSharp.nuspec -OutputDirectory ..

ECHO.
nuget.exe pack CitiesSkylinesInsights.CitiesSkylines.ColossalManaged.nuspec -OutputDirectory ..

ECHO.
nuget.exe pack CitiesSkylinesInsights.CitiesSkylines.ICities.nuspec -OutputDirectory ..

ECHO.
ECHO Build complete.
