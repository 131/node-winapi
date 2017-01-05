#include <nan.h>
#include <windows.h>




void GetLastInputInfo(const Nan::FunctionCallbackInfo<v8::Value>& info) {

  LASTINPUTINFO li;
  li.cbSize = sizeof(LASTINPUTINFO);
  ::GetLastInputInfo(&li);
  
  int elapsed = li.dwTime;

  info.GetReturnValue().Set(Nan::New(elapsed));
}

void GetTickCount(const Nan::FunctionCallbackInfo<v8::Value>& info) {
  int elapsed = ::GetTickCount();

  info.GetReturnValue().Set(Nan::New(elapsed));
}


void Init(v8::Local<v8::Object> exports) {
  exports->Set(Nan::New("GetLastInputInfo").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(GetLastInputInfo)->GetFunction());

  exports->Set(Nan::New("GetTickCount").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(GetTickCount)->GetFunction());

}





NODE_MODULE(winapi, Init)
