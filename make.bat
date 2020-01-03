npm install
npm install nw-gyp

set npm_config_runtime=nwjs
set npm_config_modules=59
nw-gyp rebuild --target=0.26.6 --arch=ia32 --verbose
