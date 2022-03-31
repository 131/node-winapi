'use strict';

var path     = require('path');
var execFile = require('child_process').execFile;
var sprintf = require('util').format;

var winapi;


var winapiCS = path.join(__dirname, 'WinAPI.exe');
try {
  winapi = require('node-gyp-build')(__dirname);
} catch(e) {
  console.log(e);
  throw Error(sprintf("Compilation of winapi has failed and there is no pre-compiled binary available for your system. Please install a supported C++11 compiler and reinstall the module 'winapi' (missing %s)") );
}



winapi.GetDisplaySettings = function(chain) {
  execFile(winapiCS, ["GetDisplaySettings"], function(err, stdout) {
    chain(err, JSON.parse(stdout));
  });

};



winapi.ReOrientDisplay = function(orientation, chain) {
  execFile(winapiCS, ["ReOrientDisplay", orientation], {}, chain);
};

winapi.MaximizeWindow = function(title, chain) {
  execFile(winapiCS, ["MaximizeWindow", title], {}, chain);
};

winapi.MinimizeWindow = function(title, chain) {
  execFile(winapiCS, ["MinimizeWindow", title], {}, chain);
};


winapi.HideWindow = function(title, chain) {
  execFile(winapiCS, ["HideWindow", title], {}, chain);
};

winapi.ShowWindow = function(title, chain) {
  execFile(winapiCS, ["ShowWindow", title], {}, chain);
};

winapi.GetDisplaysList = function(chain) {
  execFile(winapiCS, ["GetDisplaysList"], {}, function(err, stdout) {
    chain(err, JSON.parse(stdout));
  });
};



winapi.getIdleTime = function() {
  return winapi.GetTickCount() - winapi.GetLastInputInfo();
};




module.exports = winapi;
