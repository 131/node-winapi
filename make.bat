call npm install
call npm install nw-gyp


REM in vs2017 cmd line
call prebuildify --target node-webkit@0.14.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node-webkit@0.26.0 --strip --arch=ia32 --node-gyp=nw-gyp.cmd
call prebuildify --target node-webkit@0.26.6 --strip --arch=ia32 --node-gyp=nw-gyp.cmd

REM in vs2019 cmd line
call prebuildify --target node@8.17.0 --strip --arch=x64
call prebuildify --target node@12.22.9 --strip --arch=x64
