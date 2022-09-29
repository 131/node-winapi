#!/bin/bash

set -eux

# read secrets from env file, if available

if [[ -f .env ]] ; then
  export $(cat .env | xargs)
fi


out_cmd_x86=WinAPI.exe


dobuildcs=
dobuild=
dosign=
dotest=

while test $# -gt 0
do
  case "$1" in
    --build) dobuild=true
        ;;
    --build-cs) dobuildcs=true
        ;;
    --sign) dosign=true
        ;;
    --test) dotest=true
        ;;
    --*) echo "bad option $1"
        ;;
    *) echo "argument $1"
        ;;
  esac
  shift
done

sign() {
  FILE_IN="$1"
  FILE_OUT="$FILE_IN.signed"

  echo "Signing $FILE_IN"

  args=(-s -X PUT --data-binary @-)
  args=("${args[@]}"  -D -)
  args=("${args[@]}" -o "$FILE_OUT")

  response=$(cat "$FILE_IN" | curl "${args[@]}" $SIGNING_SERVER | sed 's/\r$//' )

  if echo "$response" | grep -qe "HTTP/1.. 200 " ; then
    mv "$FILE_OUT" "$FILE_IN"
    echo "$FILE_IN successfully signed."
  else
    echo  "Could not sign $FILE_IN"
    exit 1
  fi
}

if [[ ! -z "$dobuild" ]] ; then
  echo "Running application build"

  cmd.exe /c "make.bat"
fi



if [[ ! -z "$dobuildcs" ]] ; then
  echo "Running cs application build"

  if which wslpath; then
    PATH=$PATH:$(wslpath 'C:\Windows\Microsoft.NET\Framework\v4.0.30319')
  elif which cygpath; then
    PATH=$PATH:$(cygpath 'C:\Windows\Microsoft.NET\Framework\v4.0.30319')
  fi


  v4=C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319
  args=(/noconfig /nowarn:1701,1702 /nostdlib+ /errorreport:prompt /warn:0 /errorendlocation /preferreduilang:en-US /highentropyva-)
  args=("${args[@]}" /reference:$v4\\mscorlib.dll /reference:$v4\\System.dll /reference:$v4\\System.Core.dll /reference:$v4\\System.Drawing.dll /reference:$v4\\System.Web.Extensions.dll /reference:$v4\\System.Windows.Forms.dll /reference:$v4\\System.Xml.Linq.dll /reference:$v4\\System.Data.DataSetExtensions.dll /reference:$v4\\Microsoft.CSharp.dll /reference:$v4\\System.Data.dll /reference:$v4\\System.Xml.dll)

 
  args=("${args[@]}" /filealign:512 /utf8output)

  files=(src\\cs\\Program.cs src\\cs\\Properties\\AssemblyInfo.cs src\\cs\\User32.cs)

  csc.exe "${args[@]}" /platform:x86  /target:exe    /out:$out_cmd_x86 "${files[@]}"

  echo "Build output"
  ls -la $out_cmd_x86
fi

if [[ ! -z "$dosign" ]] ; then
  if [[ -z "$SIGNING_SERVER" ]] ; then
    echo "No signing server defined";
    exit 1
  fi

  echo "Signing binaries"

  sign $out_cmd_x86
fi

