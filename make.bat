REM expand path with Msbuild.exe path, helps nw-gyp.cmd
"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe > %temp%\MSBUILD_PATH && set /P MSBUILD_PATH=<%temp%\MSBUILD_PATH
for %%F in ("%MSBUILD_PATH%") do set MSBUILD_DIR=%%~dpF
set path=%path%;%MSBUILD_DIR%

where msbuild.exe

REM in vs2017 cmd line
call prebuildify --target node-webkit@0.14.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node-webkit@0.26.0 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node-webkit@0.26.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node@10.24.0 --strip --arch=ia32


REM in vs2019 cmd line
call prebuildify --target node@8.17.0 --strip --arch=x64
call prebuildify --target node@10.24.0 --strip --arch=x64
call prebuildify --target node@12.22.9 --strip --arch=x64
