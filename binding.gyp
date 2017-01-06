{
  "targets": [
    {
      "target_name": "winapi",
      "sources": [ "src/main.cc" ],
      "include_dirs": [
        "<!(node -e \"require('nan')\")"
      ]
    },

    {
      'target_name': 'action_after_build',
      'type': 'none',
      'dependencies': [ 'winapi' ],
      'conditions': [
        ['OS=="win"', {
            'actions': [
              {
                'action_name': 'move_lib',
                'inputs': [
                  '<@(PRODUCT_DIR)/winapi.node'
                ],
                'outputs': [
                  'winapi'
                ],
                'action': ['copy', '<@(PRODUCT_DIR)/winapi.node', 'winapi_<!@(node -p process.platform)_<!@(node -p process.versions.modules).node']
              }
            ]}
        ]
      ]
    }

  ]
}
