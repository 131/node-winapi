# winapi
Misc natives wrappers for nodejs

## require('winapi').getIdleTime()
Return the time the system has been idle (since last user interaction - e.g. mouse, keyboard & stuffs, see GetLastInputInfo)
This is really usefull if you want to create a screensaver/like in nodejs / node-webkit.

```
var winapi = require('winapi');

console.log("System is idle since %s", winapi.getIdleTime() );

```



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


# Credits
* [131](mailto:131.js@cloudyks.org)


# Keywords / shout box
screensaver, windows api, winuser.h, GetLastInputInfo, activity monitor, inactivity trigger, idle timer, system uptime, sytem bootime


