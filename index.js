'use strict';

const path     = require('path');
const execFile = require('child_process').execFile;


const winapiCS = path.join(__dirname, 'WinAPI.exe');

var winapi;

const engine = process.versions.nw ? "nwjs" : "node";
const module_path = `./winapi_${process.platform}_${process.versions.modules}_${engine}.node`;
try {
   winapi = require(module_path);
} catch (e) {
  throw Error(`Compilation of winapi has failed and there is no pre-compiled binary available for your system. Please install a supported C++11 compiler and reinstall the module 'winapi' (missing ${module_path})`);
}



winapi.GetDisplaySettings = function(chain){
  execFile(winapiCS, ["GetDisplaySettings"], function(err, stdout){
   chain(err, JSON.parse(stdout));
  });
  
}



winapi.ReOrientDisplay = function(orientation, chain){
  execFile(winapiCS, ["ReOrientDisplay", orientation], {}, chain);
}



winapi.GetDisplaysList = function( chain){
  execFile(winapiCS, ["GetDisplaysList"], {}, function(err, stdout){
   chain(err, JSON.parse(stdout));
  });
}



var bootTime;

winapi.getIdleTime = function(){
  if(!bootTime)
     bootTime = Date.now() - winapi.GetTickCount();

 return Date.now() - bootTime - winapi.GetLastInputInfo();
}




module.exports = winapi;