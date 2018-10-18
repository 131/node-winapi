"use strict";

const winapi = require('./');


var start = winapi.GetLastInputInfo();


console.log(start );


winapi.CreateJobGroup();

var cp = require('child_process');

var child = cp.spawn("notepad.exe",  {
  detached: true,
  stdio: 'ignore'
});

child.unref();


setInterval(Function.prototype, 1000);