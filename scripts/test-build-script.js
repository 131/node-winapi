'use strict';

const assert = require('assert');
const fs = require('fs');
const os = require('os');
const path = require('path');
const {spawnSync} = require('child_process');

const root = fs.mkdtempSync(path.join(os.tmpdir(), 'winapi-build-test-'));
const make = path.resolve(__dirname, '../make');
fs.writeFileSync(path.join(root, '.env'), 'SIGNING_SERVER=http://obsolete.invalid/old-token\r\n');
fs.writeFileSync(path.join(root, 'curl'), `#!/bin/bash
set -eu
output=
while [ "$#" -gt 0 ]; do
  if [ "$1" = "-o" ]; then output="$2"; shift; fi
  url="$1"
  shift
done
test "$url" = "http://signer.invalid/current-token" || exit 91
if [ "\${MOCK_FAIL:-}" = "1" ]; then exit 7; fi
if [ "\${MOCK_EMPTY:-}" != "1" ]; then printf signed > "$output"; else : > "$output"; fi
printf '%s' "\${MOCK_STATUS:-200}"
`, {mode: 0o700});

function run(env) {
  fs.writeFileSync(path.join(root, 'WinAPI.exe'), 'original');
  return spawnSync('bash', [make, '--sign'], {
    cwd: root,
    env: {...process.env, PATH: root + path.delimiter + process.env.PATH,
      SIGNING_SERVER: 'http://signer.invalid/current-token', ...env},
    encoding: 'utf8',
  });
}

let result = run({});
assert.strictEqual(result.status, 0, result.stderr);
assert.strictEqual(fs.readFileSync(path.join(root, 'WinAPI.exe'), 'utf8'), 'signed');
assert(!/current-token|old-token/.test(result.stdout + result.stderr));

for(const env of [{MOCK_STATUS: '403'}, {MOCK_FAIL: '1'}, {MOCK_EMPTY: '1'}]) {
  result = run(env);
  assert.notStrictEqual(result.status, 0);
  assert.strictEqual(fs.readFileSync(path.join(root, 'WinAPI.exe'), 'utf8'), 'original');
  assert(!fs.existsSync(path.join(root, 'WinAPI.exe.signed')));
  assert(!/current-token|old-token/.test(result.stdout + result.stderr));
}
result = spawnSync('bash', [make, '--unknown'], {cwd: root});
assert.notStrictEqual(result.status, 0);
console.log('Build script: environment precedence, signing failures, secret safety and options OK');
