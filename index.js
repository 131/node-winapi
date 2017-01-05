'use strict';

var winapi;
try {
   winapi = require('bindings')('winapi');

} catch (e) {
  throw Error("Could not load winapi");
}




var bootTime;

winapi.getIdleTime = function(){
  if(!bootTime)
     bootTime = Date.now() - winapi.GetTickCount();

  return Date.now() - bootTime - winapi.GetLastInputInfo();
}




module.exports = winapi;