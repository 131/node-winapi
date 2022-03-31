{"targets": [

  {
    "target_name": "winapi",
    "sources": [ "src/main.cc" ],
    "include_dirs": [
      "<!(node -e \"require('nan')\")"
    ],
  }
]}