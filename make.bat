call npm install
call npm install -g nw-gyp prebuildify node-gyp

"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe

set path=%path%;C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin
set path=%path%;C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin

where msbuild.exe

REM in vs2017 cmd line
call prebuildify --target node-webkit@0.14.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node-webkit@0.26.0 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node-webkit@0.26.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd

REM in vs2019 cmd line
call prebuildify --target node@8.17.0 --strip --arch=x64
call prebuildify --target node@12.22.9 --strip --arch=x64
