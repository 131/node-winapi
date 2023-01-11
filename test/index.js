"use strict";

const os   = require('os');

const expect = require("expect.js");

const winapi = require('../');


const isContainer = (os.userInfo().username == 'ContainerAdministrator');

describe("Initial test suite", function(){

  it("should list pids", function(){

    var foo = winapi.GetChildrenProcess( winapi.GetParentProcess() );

    expect(foo.indexOf(process.pid)).not.to.eql(-1);
  });


  it("Should measure 1s of inactity", function(done){

      var start = winapi.GetLastInputInfo();
      console.log("DONT MOVE");

      setTimeout(function(){
        var now = winapi.GetLastInputInfo();
        expect(now).to.equal(start);
        done();
      }, 1000);

  });


  if(!isContainer) it("Should measure actity", function(done){
    this.timeout(5000);

    var start = winapi.GetLastInputInfo();

    console.log("Press any key, you have 2s to comply");

    setTimeout(function(){
      winapi.moveMouse(0,0);
    }, 1000);
    
    setTimeout(function(){
      winapi.SetCursorPos(0,0);
      var now = winapi.GetLastInputInfo();
      expect(now).not.to.equal(start);

      console.log("You took %s to do stuff", (now - start) / 1000)
      done();
    }, 2000);

  });


  if(!isContainer) it("Last idle Time should be recent", function(){
      var since = winapi.getIdleTime();
      console.log("System idle since ", since/1000);
      expect(since < 2000).to.be.ok();

  });


  it("Get system time", function() {
      var value = winapi.GetUTCTime();
      var now   = new Date();

      console.log("System time is ", value);

      expect(value).to.be.ok();
      expect(value[0]).to.eql(now.getUTCFullYear());
      expect(value[1]).to.eql(now.getUTCMonth());
      expect(value[2]).to.eql(now.getUTCDate());
      expect(value[3]).to.eql(now.getUTCHours());
  });

  /*if(!isContainer) it("Set system time", function() {
      var now      = new Date();
      var new_date = [
        now.getUTCFullYear() + 1,
        now.getUTCMonth(),
        now.getUTCDate(),
        now.getUTCHours(),
        now.getUTCMinutes(),
        now.getUTCSeconds(),
        now.getUTCMilliseconds()
      ];

      var after = new Date();

      console.log('sending', new_date, 'to set date');

      var result = winapi.SetUTCTime(new_date);

      console.log("compare dates :", now, after);

      expect(result).to.be.ok();
      expect(now.getUTCFullYear()).to.eql(after.getUTCFullYear() + 1);
  });*/

  this.timeout(20 * 1000);

  if(false) it("should check screen orientation", function(done){
    var initial;

    winapi.GetDisplaySettings(function(err, value){
        initial = value.Orientation;
        var newval = (initial+1) %4;
        console.log({initial, newval});
        winapi.ReOrientDisplay(newval, function(err, value){

        winapi.GetDisplaySettings(function(err, value){
          expect(value.Orientation).to.eql(newval);
          winapi.ReOrientDisplay(initial, done);
        });
      });
    });
  });




});