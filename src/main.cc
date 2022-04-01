#include <nan.h>
#include <windows.h>
#include <winuser.h>
#include <tlhelp32.h>



// The width of the virtual screen, in pixels.
static int vscreenWidth = -1; // not initialized

// The height of the virtual screen, in pixels.
static int vscreenHeight = -1; // not initialized

// The coordinates for the left side of the virtual screen.
static int vscreenMinX = 0;

// The coordinates for the top of the virtual screen.
static int vscreenMinY = 0;



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
    pid = Nan::To<int32_t>(info[0]).ToChecked();
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


void SetCursorPos(const Nan::FunctionCallbackInfo<v8::Value>& info) {
  int x = Nan::To<int32_t>(info[0]).ToChecked();;
  int y = Nan::To<int32_t>(info[1]).ToChecked();;

  SetCursorPos(x, y);
}


void updateScreenMetrics()
{
  vscreenWidth = GetSystemMetrics(SM_CXVIRTUALSCREEN);
  vscreenHeight = GetSystemMetrics(SM_CYVIRTUALSCREEN);
  vscreenMinX = GetSystemMetrics(SM_XVIRTUALSCREEN);
  vscreenMinY = GetSystemMetrics(SM_YVIRTUALSCREEN);
}


void moveMouse(const Nan::FunctionCallbackInfo<v8::Value>& info) {


  if(vscreenWidth<0 || vscreenHeight<0)
    updateScreenMetrics();

  int x = Nan::To<int32_t>(info[0]).ToChecked();
  int y = Nan::To<int32_t>(info[1]).ToChecked();

  INPUT mouseInput = {0};
  mouseInput.type = INPUT_MOUSE;
  mouseInput.mi.dx = x;
  mouseInput.mi.dy = y;
  mouseInput.mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE | MOUSEEVENTF_VIRTUALDESK;
  mouseInput.mi.time = 0; //System will provide the timestamp

  SendInput(1, &mouseInput, sizeof(mouseInput));
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
    pid = Nan::To<int32_t>(info[0]).ToChecked();
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
  v8::Local<v8::Context> context = exports->CreationContext();

  exports->Set(context, Nan::New("CreateJobGroup").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(CreateJobGroup)->GetFunction(context).ToLocalChecked());
  exports->Set(context, Nan::New("GetLastInputInfo").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(GetLastInputInfo)->GetFunction(context).ToLocalChecked());
  exports->Set(context, Nan::New("GetTickCount").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(GetTickCount)->GetFunction(context).ToLocalChecked());
  exports->Set(context, Nan::New("GetChildrenProcess").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(ListProcessPID)->GetFunction(context).ToLocalChecked());
  exports->Set(context, Nan::New("GetParentProcess").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(getParentPid)->GetFunction(context).ToLocalChecked());


  exports->Set(context, Nan::New("SetCursorPos").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(SetCursorPos)->GetFunction(context).ToLocalChecked());
  exports->Set(context, Nan::New("moveMouse").ToLocalChecked(), Nan::New<v8::FunctionTemplate>(moveMouse)->GetFunction(context).ToLocalChecked());
}






NODE_MODULE(winapi, Init)
