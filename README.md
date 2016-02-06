# winapi
Misc natives wrappers for nodejs

## require('winapi').GetLastInputInfo()
Use this native binding to get system last input time ([see MSDN](https://msdn.microsoft.com/en-us/library/windows/desktop/ms646302%28v=vs.85%29.aspx)

```
var winapi = require('winapi');

console.log("Last input time was %s", winapi.GetLastInputInfo() );

```


# Credits
* [131](mailto:131.js@cloudyks.org)


# Keywords / shout box
screensaver, windows api, winuser.h, GetLastInputInfo

