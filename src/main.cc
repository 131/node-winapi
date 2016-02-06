#include <nan.h>
#include <node.h>
#include <v8.h>
#include <windows.h>


using namespace v8;

void GetLastInputInfo(const v8::FunctionCallbackInfo<Value>& info) {
  Isolate* isolate = Isolate::GetCurrent();
  HandleScope scope(isolate);

  LASTINPUTINFO li;
  li.cbSize = sizeof(LASTINPUTINFO);
  ::GetLastInputInfo(&li);

  int elapsed = li.dwTime;

  v8::Local<v8::Number> num = Nan::New(elapsed);

  info.GetReturnValue().Set(num);
}

void GetTickCount(const v8::FunctionCallbackInfo<Value>& info) {
  Isolate* isolate = Isolate::GetCurrent();
  HandleScope scope(isolate);


  int elapsed = ::GetTickCount();

  v8::Local<v8::Number> num = Nan::New(elapsed);

  info.GetReturnValue().Set(num);
}




void Init(Handle<Object> exports) {
  Isolate* isolate = Isolate::GetCurrent();
  exports->Set(String::NewFromUtf8(isolate, "GetLastInputInfo"),
      FunctionTemplate::New(isolate, GetLastInputInfo)->GetFunction());

  exports->Set(String::NewFromUtf8(isolate, "GetTickCount"),
      FunctionTemplate::New(isolate, GetTickCount)->GetFunction());

}

NODE_MODULE(winapi, Init)
