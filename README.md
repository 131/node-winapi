
# winapi
Misc natives MS Windows API wrappers for nodejs & nwjs

[![Version](https://img.shields.io/npm/v/winapi.svg)](https://www.npmjs.com/package/winapi)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](http://opensource.org/licenses/MIT)
[![Available platform](https://img.shields.io/badge/platform-nwjs-blue.svg)](https://www.npmjs.com/package/winapi) [![Available platform](https://img.shields.io/badge/platform-win32-blue.svg)](https://www.npmjs.com/package/winapi)
[![Code style](https://img.shields.io/badge/code%2fstyle-ivs-green.svg)](https://www.npmjs.com/package/eslint-plugin-ivs)



# API

## Building and signing

The Windows test workflow builds the native prebuilds and `WinAPI.exe` without
signing. Tests use `PREBUILDS_ONLY=1` so a leftover `build/Release` binary cannot
hide a missing or incompatible packaged prebuild.

The publish workflow builds and signs `WinAPI.exe` through
`131/gh-actions/sign-binaries@v1`, then checks its Authenticode signature on
Windows. Set the repository secret `SIGNING_SERVER` to the current Janus
`winapi` preset URL supplied by the administrator. Never commit that URL's token.
Janus must reload its configuration after a preset is created or changed.

`workflow_dispatch` produces a signed artifact without publishing npm or a
GitHub release. A `v<package.json version>` tag additionally publishes npm
(repository secret `NPM_TOKEN`) and the executable/prebuild archive to GitHub.
No version is bumped by the workflow.

For local signing, use `SIGNING_SERVER=… bash make --sign`. An explicit environment
value takes precedence over `.env`; the old local URL/token may be obsolete.
The script does not trace secrets and preserves the original executable if
signing fails. `bash make --build --build-cs --test` builds and runs the tests.

Build-script regression tests run on Linux: `node scripts/test-build-script.js`.

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
* Code signing, courtesy of IVS Group.


# Keywords / shout box
screensaver, windows api, winuser.h, GetLastInputInfo, activity monitor, inactivity trigger, idle timer, system uptime, sytem bootime


