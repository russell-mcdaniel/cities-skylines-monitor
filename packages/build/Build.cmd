@ECHO OFF

ECHO.
ECHO Building NuGet packages...

ECHO.
nuget.exe pack CitiesSkylines.Assembly-CSharp.nuspec -OutputDirectory ..

ECHO.
nuget.exe pack CitiesSkylines.ColossalManaged.nuspec -OutputDirectory ..

ECHO.
nuget.exe pack CitiesSkylines.ICities.nuspec -OutputDirectory ..

ECHO.
nuget.exe pack CitiesSkylines.UnityEngine.nuspec -OutputDirectory ..

ECHO.
nuget.exe pack CitiesSkylines.UnityEngine.UI.nuspec -OutputDirectory ..

ECHO.
ECHO Build complete.
