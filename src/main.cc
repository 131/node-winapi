#include <nan.h>
#include <windows.h>


using namespace v8;



NAN_METHOD(GetLastInputInfo)
{
  NanScope();
  
  LASTINPUTINFO li;
  li.cbSize = sizeof(LASTINPUTINFO);
  ::GetLastInputInfo(&li);
  
  int elapsed = li.dwTime;
  
  NanReturnValue(NanNew<v8::Number>(elapsed));
}

NAN_METHOD(GetTickCount)
{
  NanScope();
  
  int elapsed = ::GetTickCount();

  NanReturnValue(NanNew<v8::Number>(elapsed));
}



void Init(Handle<Object> exports) {
  NanScope();
  
  exports->Set(NanNew<v8::String>("GetLastInputInfo"), NanNew<v8::FunctionTemplate>(GetLastInputInfo)->GetFunction());
  exports->Set(NanNew<v8::String>("GetTickCount"), NanNew<v8::FunctionTemplate>(GetTickCount)->GetFunction());
}




NODE_MODULE(winapi, Init)
