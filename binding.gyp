{"targets": [

  {
    "target_name": "winapi",
    "sources": [ "src/main.cc" ],
    "include_dirs": [
      "<!(node -e \"require('nan')\")"
    ],
  },


  {
    "target_name": "other_library",
    "type": "none",
    'dependencies': [ 'winapi'],
    "actions": [ {
      "action_name": "build_other_library",
      "inputs": [],
      "outputs": [ "winapi_target" ],
      "action": [ 'node -p "fs.copyFileSync(process.argv[1], `${process.argv[2]}_${process.platform}_${process.env.npm_config_modules||process.versions.modules}_${process.env.npm_config_runtime||process.release.name}.node`)"', '<@(PRODUCT_DIR)/winapi.node', './winapi']
     }]
  }

]}