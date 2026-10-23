REM expand path with Msbuild.exe path, helps nw-gyp.cmd
"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe > %temp%\MSBUILD_PATH && set /P MSBUILD_PATH=<%temp%\MSBUILD_PATH
for %%F in ("%MSBUILD_PATH%") do set MSBUILD_DIR=%%~dpF
set path=%path%;%MSBUILD_DIR%

where msbuild.exe

REM in vs2017 cmd line
REM Legacy NW headers invoke "python" directly, not only the PYTHON variable.
setlocal
set "PYTHON=C:\Python27\python.exe"
set "npm_config_python=%PYTHON%"
set "PATH=C:\Python27;%PATH%"
python --version
if errorlevel 1 exit /b 1
call prebuildify --target node-webkit@0.14.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
if errorlevel 1 exit /b 1
call prebuildify --target node-webkit@0.26.0 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
if errorlevel 1 exit /b 1
call prebuildify --target node-webkit@0.26.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
if errorlevel 1 exit /b 1
endlocal

REM Restore setup-python's Python 3 for node-gyp@9 and native Node targets.
call prebuildify --target node@10.24.0 --strip --arch=ia32
if errorlevel 1 exit /b 1


REM in vs2019 cmd line

call prebuildify --target node@8.17.0 --strip --arch=x64
if errorlevel 1 exit /b 1
if not exist "prebuilds\win32-x64\winapi.node" exit /b 1
move /Y "prebuilds\win32-x64\winapi.node" "prebuilds\win32-x64\winapi.abi57.node"
if errorlevel 1 exit /b 1

call prebuildify --target node@16.20.2 --strip --arch=x64
if errorlevel 1 exit /b 1
if not exist "prebuilds\win32-x64\winapi.node" exit /b 1
move /Y "prebuilds\win32-x64\winapi.node" "prebuilds\win32-x64\winapi.abi93.node"
if errorlevel 1 exit /b 1

call prebuildify --target node@24.16.0 --strip --arch=x64
if errorlevel 1 exit /b 1
if not exist "prebuilds\win32-x64\winapi.node" exit /b 1
move /Y "prebuilds\win32-x64\winapi.node" "prebuilds\win32-x64\winapi.abi137.node"
if errorlevel 1 exit /b 1

dir "prebuilds\win32-x64"
