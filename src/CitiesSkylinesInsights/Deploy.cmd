@ECHO OFF

ECHO.
ECHO Creating the mod path...

SET MODPATH=%LOCALAPPDATA%\Colossal Order\Cities_Skylines\Addons\Mods\CitiesSkylinesInsights

IF NOT EXIST "%MODPATH%" (
    MKDIR "%MODPATH%"
    ECHO %MODPATH% created.
) ELSE (
    ECHO %MODPATH% already exists.
)

ECHO.
ECHO Removing existing files...

DEL /F /S /Q "%MODPATH%\*.*"

ECHO.
ECHO Copying files...

XCOPY /E "bin\Debug\CitiesSkylinesInsights.*" "%MODPATH%"

ECHO.
ECHO Deployment complete.
