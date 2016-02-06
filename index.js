'use strict';

var winapi;
try {
   winapi = require('bindings')('winapi');

} catch (e) {
  winapi = {
      GetLastInputInfo : function(){
        return Date.now();
      },
      GetTickCount : function(){
        return Date.now();
      }

  };
}




var bootTime;

winapi.getIdleTime = function(){
  if(!bootTime)
     bootTime = Date.now() - winapi.GetTickCount();

  return Date.now() - bootTime - winapi.GetLastInputInfo();
}




module.exports = winapi;