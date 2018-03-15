{
  "targets": [
    {
      "target_name": "winapi_<!@(node -p process.platform)_<!@(node -p process.versions.modules)",
      "sources": [ "src/main.cc" ],
      "include_dirs": [
        "<!(node -e \"require('nan')\")"
      ]
    },

    {
      'target_name': 'action_after_build',
      'type': 'none',
      'dependencies': [ 'winapi_<!@(node -p process.platform)_<!@(node -p process.versions.modules)' ],

       "copies":
           [
              {
                 'destination': '.',
                 'files': ['<@(PRODUCT_DIR)/winapi_<!@(node -p process.platform)_<!@(node -p process.versions.modules).node']
              }
           ]

    }

  ]
}