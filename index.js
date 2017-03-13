'use strict';
var exec = require('child_process').exec;
var path = require('path');


var winapi;
try {
   winapi = require(`./winapi_${process.platform}_${process.versions.modules}`);
} catch (e) {
  throw Error("Compilation of winapi has failed and there is no pre-compiled binary available for your system. Please install a supported C++11 compiler and reinstall the module 'winapi'");
}

var winapiCS = path.join(__dirname, 'WinAPI.exe');


winapi.GetDisplaySettings = function(chain){
  exec([winapiCS, "GetDisplaySettings"].join(' '), function(err, stdout){
    chain(err, JSON.parse(stdout));
  });
  
}



winapi.ReOrientDisplay = function(orientation, chain){
  exec([winapiCS, "ReOrientDisplay", orientation].join(' '), {}, chain);
}



var bootTime;

winapi.getIdleTime = function(){
  if(!bootTime)
     bootTime = Date.now() - winapi.GetTickCount();

  return Date.now() - bootTime - winapi.GetLastInputInfo();
}




module.exports = winapi;