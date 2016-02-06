'use strict';

try {
  module.exports = require('bindings')('winapi');
} catch (e) {
  module.exports = {
      GetLastInputInfo : function(){
        return Date.now();
      }
  };
}
