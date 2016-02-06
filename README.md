# winapi
Misc natives wrappers for nodejs

## require('winapi').GetLastInputInfo()
Use this native binding to get system last input time ([see MSDN](https://msdn.microsoft.com/en-us/library/windows/desktop/ms646302%28v=vs.85%29.aspx) )
This is really usefull if you want to create a screensaver/like in nodejs / node-webkit.

```
var winapi = require('winapi');

console.log("Last input time is %s", winapi.GetLastInputInfo() );

setTimeout(function(){
  //do not move, it wont change !
  console.log("Last input time is %s", winapi.GetLastInputInfo() );
}, 1000);

```


# Credits
* [131](mailto:131.js@cloudyks.org)


# Keywords / shout box
screensaver, windows api, winuser.h, GetLastInputInfo

