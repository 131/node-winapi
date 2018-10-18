
# winapi
Misc natives MS Windows API wrappers for nodejs & nwjs

[![Version](https://img.shields.io/npm/v/winapi.svg)](https://www.npmjs.com/package/winapi)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](http://opensource.org/licenses/MIT)
[![Available platform](https://img.shields.io/badge/platform-nwjs-blue.svg)](https://www.npmjs.com/package/winapi) [![Available platform](https://img.shields.io/badge/platform-win32-blue.svg)](https://www.npmjs.com/package/winapi)
[![Code style](https://img.shields.io/badge/code%2fstyle-ivs-green.svg)](https://www.npmjs.com/package/eslint-plugin-ivs)



# API

## require('winapi').getIdleTime()
Return the time the system has been idle (since last user interaction - e.g. mouse, keyboard & stuffs, see GetLastInputInfo)
This is really usefull if you want to create a screensaver/like in nodejs / node-webkit.

```
var winapi = require('winapi');

console.log("System is idle since %s", winapi.getIdleTime() );

```

## require('winapi').CreateJobGroup()
Create a [job group](https://docs.microsoft.com/fr-fr/windows/desktop/ProcThread/job-objects) with current process and all future child_process. Use this to kill zombies. Like Rick.

```
var winapi = require('winapi');

winapi.CreateJobGroup();

//you can now spawn subprocess and they'll be killed once you died, windows will clean everything up

```
* (checkout my [dispatcher](https://github.com/131/dispatcher) project for inspiration)
* Available for node 8, nw 26 and nw 14. (i did not re-compile other platform binaries, do it by yourself...)




## require('winapi').GetLastInputInfo()
Use this native binding to get system last input time ([see MSDN](https://msdn.microsoft.com/en-us/library/windows/desktop/ms646302%28v=vs.85%29.aspx) )

```
var winapi = require('winapi');

console.log("Last input time is %s", winapi.GetLastInputInfo() );

setTimeout(function(){
  //do not move, it wont change !
  console.log("Last input time is %s", winapi.GetLastInputInfo() );
}, 1000);

```


## require('winapi').GetChildrenProcess([parentProcessId])
List all children process (of specified parent PID, default to current process)


## require('winapi').GetParentProcess([childProcessId])
Get a process parent PID (of specified process PID, default to current process)



## require('winapi').GetTickCount()
Retrieves the number of milliseconds that have elapsed since the system was started (uptime). ([see MSDN](https://msdn.microsoft.com/en-us/library/windows/desktop/ms724408%28v=vs.85%29.aspx) )

### require('winapi').GetDisplaysList(console.log)
List all connected screens

# Credits
* [131](mailto:131.js@cloudyks.org)


# Keywords / shout box
screensaver, windows api, winuser.h, GetLastInputInfo, activity monitor, inactivity trigger, idle timer, system uptime, sytem bootime


