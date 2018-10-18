var cp = require('child_process');

const gui   = window.require('nw.gui');

var winapi = require('../../');


winapi.CreateJobGroup();


setInterval(function() {
  gui.Window.get().window.console.log("loo", winapi.GetLastInputInfo())
  //console.log("Last input is " + winapi.GetLastInputInfo());
}, 1000);

  gui.Window.get().showDevTools();


var child = cp.spawn("notepad.exe",  {
  detached: true,
  stdio: 'ignore'
});

child.unref();

