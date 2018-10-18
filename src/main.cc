#include <nan.h>
#include <windows.h>
#include <tlhelp32.h>




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


void CreateJobGroup(const Nan::FunctionCallbackInfo<v8::Value>& info) {

  HANDLE                               hJob;
  HANDLE                               currentProcess;
  JOBOBJECT_EXTENDED_LIMIT_INFORMATION jeli = { 0 };
  PROCESS_INFORMATION                  pi   = { 0 };
  STARTUPINFO                          si   = { 0 };

  //Create a job object.
  hJob = CreateJobObject(NULL, NULL);

  //kill all processes associated with the job to terminate when the last handle to the job is closed.

  jeli.BasicLimitInformation.LimitFlags = JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;
  SetInformationJobObject(hJob, JobObjectExtendedLimitInformation, &jeli, sizeof(jeli));
  currentProcess = GetCurrentProcess();

  AssignProcessToJobObject(hJob, currentProcess);

  info.GetReturnValue().Set(Nan::Undefined());
}




void ListProcessPID(const Nan::FunctionCallbackInfo<v8::Value>& info) {

  int pid = -1;

  if (info.Length() < 1)
    pid = GetCurrentProcessId();
  else {
    if (!info[0]->IsNumber() ) {
      Nan::ThrowTypeError("Wrong arguments");
      return;
    }
    pid = info[0]->NumberValue();
  }


  HANDLE h = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
  PROCESSENTRY32 pe = { 0 };
  pe.dwSize = sizeof(PROCESSENTRY32);


  v8::Local<v8::Array> children = Nan::New<v8::Array>();

  int i = 0;


  if( Process32First(h, &pe)) {
    do {
      if (pe.th32ParentProcessID == pid)
        children->Set(i++, Nan::New( (int) pe.th32ProcessID) );
    } while( Process32Next(h, &pe));
  }

  CloseHandle(h);

  info.GetReturnValue().Set(children);
}



void getParentPid(const Nan::FunctionCallbackInfo<v8::Value>& info) {

  int pid = -1;

  if (info.Length() < 1)
    pid = GetCurrentProcessId();
  else {
    if (!info[0]->IsNumber() ) {
      Nan::ThrowTypeError("Wrong arguments");
      return;
    }
    pid = info[0]->NumberValue();
  }

  int parentPid = -1;

  HANDLE h = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
  PROCESSENTRY32 pe = { 0 };
  pe.dwSize = sizeof(PROCESSENTRY32);

  if( Process32First(h, &pe)) {
    do {
      if (pe.th32ProcessID == pid)
        parentPid =  pe.th32ParentProcessID;
    } while( Process32Next(h, &pe));
  }

  CloseHandle(h);

  info.GetReturnValue().Set(parentPid);
}



void Init(v8::Local<v8::Object> exports) {
  exports->Set(Nan::New("CreateJobGroup").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(CreateJobGroup)->GetFunction());
  exports->Set(Nan::New("GetLastInputInfo").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(GetLastInputInfo)->GetFunction());
  exports->Set(Nan::New("GetTickCount").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(GetTickCount)->GetFunction());
  exports->Set(Nan::New("GetChildrenProcess").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(ListProcessPID)->GetFunction());
  exports->Set(Nan::New("GetParentProcess").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(getParentPid)->GetFunction());

}





NODE_MODULE(winapi, Init)
