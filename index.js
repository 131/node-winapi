'use strict';

var winapi;
try {
   winapi = require(`./winapi_${process.platform}_${process.versions.modules}`);
} catch (e) {
  throw Error("Compilation of winapi has failed and there is no pre-compiled binary available for your system. Please install a supported C++11 compiler and reinstall the module 'winapi'");
}




var bootTime;

winapi.getIdleTime = function(){
  if(!bootTime)
     bootTime = Date.now() - winapi.GetTickCount();

  return Date.now() - bootTime - winapi.GetLastInputInfo();
}




module.exports = winapi;