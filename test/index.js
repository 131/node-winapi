"use strict";

var expect   = require("expect.js");

var winapi = require('../');

describe("Initial test suite", function(){


  it("Should measure 1s of inactity", function(done){

      var start = winapi.GetLastInputInfo();
      console.log("DONT MOVE");

      setTimeout(function(){
        var now = winapi.GetLastInputInfo();
        expect(now).to.equal(start);
        done();
      }, 1000);

  });


  it("Should measure actity", function(done){
    this.timeout(5000);

    var start = winapi.GetLastInputInfo();

    console.log("Press any key, you have 2s to comply");
    setTimeout(function(){
      var now = winapi.GetLastInputInfo();
      expect(now).not.to.equal(start);

      console.log("You took %s to do stuff", (now - start) / 1000)
      done();
    }, 2000);

  });


  it("Last idle Time should be recent", function(){

      var since = winapi.getIdleTime();
      console.log("System idle since ", since/1000);
      expect(since < 2000).to.be.ok();

  });



});