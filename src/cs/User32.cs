using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace WinAPI
{


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;

        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct TBBUTTON
    {
        public Int32 iBitmap;
        public Int32 idCommand;
        public byte fsState;
        public byte fsStyle;
        //		[ MarshalAs( UnmanagedType.ByValArray, SizeConst=2 ) ]
        //		public byte[] bReserved;
        public byte bReserved1;
        public byte bReserved2;
        public UInt32 dwData;
        public IntPtr iString;
    };





    public class User32
    {

        #region consts

        // constants
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int DISP_CHANGE_SUCCESSFUL = 0;
        public const int DISP_CHANGE_BADDUALVIEW = -6;
        public const int DISP_CHANGE_BADFLAGS = -4;
        public const int DISP_CHANGE_BADMODE = -2;
        public const int DISP_CHANGE_BADPARAM = -5;
        public const int DISP_CHANGE_FAILED = -1;
        public const int DISP_CHANGE_NOTUPDATED = -3;
        public const int DISP_CHANGE_RESTART = 1;

        public enum Orientation
        {
            DMDO_DEFAULT = 0,
            DMDO_90 = 1,
            DMDO_180 = 2,
            DMDO_270 = 3
        };


        internal static Dictionary<int, Orientation> FlipOrientation = new Dictionary<int, Orientation>
    {
      {0, Orientation.DMDO_DEFAULT},
      {1, Orientation.DMDO_90},
      {2, Orientation.DMDO_180},
      {3, Orientation.DMDO_270},
    };



        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const uint MOUSEEVENTF_MOVE = 0x0001;


        public const uint VM_CLOSE = 0x0010;
        public const uint VM_GETICON = 0x007F;
        public const uint VM_KEYDOWN = 0x0100;
        public const uint VM_COMMAND = 0x0111;
        public const uint VM_USER = 0x0400; // 0x0400 - 0x7FFF
        public const uint VM_APP = 0x8000; // 0x8000 - 0xBFFF

        public const uint TB_GETBUTTON = 0x0400 + 23;
        public const uint TB_BUTTONCOUNT = 0x0400 + 24;
        public const uint TB_CUSTOMIZE = 0x0400 + 27;
        public const uint TB_GETBUTTONTEXTA = 0x0400 + 45;
        public const uint TB_GETBUTTONTEXTW = 0x0400 + 75;


        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;

        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;

        public const uint WS_CHILD = 0x40000000;
        public const uint WS_POPUP = 0x80000000;
        public const int WM_UPDATEUISTATE = 0x0127;
        public const int UIS_INITIALIZE = 3;


        public const uint WS_BORDER = 0x00800000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const uint WS_EX_WINDOWEDGE = 0x00000100;
        public const uint WS_EX_CLIENTEDGE = 0x00000200;
        public const uint WS_EX_STATICEDGE = 0x00020000;
        public const uint WS_EX_TOOLWINDOW = 0x00000080;
        public const uint WS_EX_TRANSPARENT = 0x00000020;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_CLIPCHILDREN = 0x02000000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const uint WS_EX_APPWINDOW = 0x00040000;
        public const uint WS_EX_TOPMOST = 0x00000008;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_STYLE = -16;
        public const long GWL_HWNDPARENT = -8;

        //GetWindow()..
        public const uint GW_HWNDFIRST = 0;
        public const uint GW_HWNDLAST = 1;
        public const uint GW_HWNDNEXT = 2;
        public const uint GW_HWNDPREV = 3;
        public const uint GW_OWNER = 4;
        public const uint GW_CHILD = 5;
        public const uint GW_MAX = 5;

        public const long GCL_MENUNAME = -8;
        public const long GCL_HBRBACKGROUND = -10;
        public const long GCL_HCURSOR = -12;
        public const long GCL_HICON = -14;
        public const long GCL_HMODULE = -16;
        public const long GCL_CBWNDEXTRA = -18;
        public const long GCL_CBCLSEXTRA = -20;
        public const long GCL_WNDPROC = -24;
        public const long GCL_STYLE = -26;
        public const long GCW_ATOM = -32;


        public const uint SPIF_UPDATEINIFILE = 0x01;
        public const uint SPIF_SENDCHANGE = 0x02;
        public const uint SPIF_SENDWININICHANGE = 0x02;

        public const uint SPI_GETBEEP = 0x0001;
        public const uint SPI_SETBEEP = 0x0002;
        public const uint SPI_GETMOUSE = 0x0003;
        public const uint SPI_SETMOUSE = 0x0004;
        public const uint SPI_GETBORDER = 0x0005;
        public const uint SPI_SETBORDER = 0x0006;
        public const uint SPI_GETKEYBOARDSPEED = 0x000A;
        public const uint SPI_SETKEYBOARDSPEED = 0x000B;
        public const uint SPI_LANGDRIVER = 0x000C;
        public const uint SPI_ICONHORIZONTALSPACING = 0x000D;
        public const uint SPI_GETSCREENSAVETIMEOUT = 0x000E;
        public const uint SPI_SETSCREENSAVETIMEOUT = 0x000F;
        public const uint SPI_GETSCREENSAVEACTIVE = 0x0010;
        public const uint SPI_SETSCREENSAVEACTIVE = 0x0011;
        public const uint SPI_GETGRIDGRANULARITY = 0x0012;
        public const uint SPI_SETGRIDGRANULARITY = 0x0013;
        public const uint SPI_SETDESKWALLPAPER = 0x0014;
        public const uint SPI_SETDESKPATTERN = 0x0015;
        public const uint SPI_GETKEYBOARDDELAY = 0x0016;
        public const uint SPI_SETKEYBOARDDELAY = 0x0017;
        public const uint SPI_ICONVERTICALSPACING = 0x0018;
        public const uint SPI_GETICONTITLEWRAP = 0x0019;
        public const uint SPI_SETICONTITLEWRAP = 0x001A;
        public const uint SPI_GETMENUDROPALIGNMENT = 0x001B;
        public const uint SPI_SETMENUDROPALIGNMENT = 0x001C;
        public const uint SPI_SETDOUBLECLKWIDTH = 0x001D;
        public const uint SPI_SETDOUBLECLKHEIGHT = 0x001E;
        public const uint SPI_GETICONTITLELOGFONT = 0x001F;
        public const uint SPI_SETDOUBLECLICKTIME = 0x0020;
        public const uint SPI_SETMOUSEBUTTONSWAP = 0x0021;
        public const uint SPI_SETICONTITLELOGFONT = 0x0022;
        public const uint SPI_GETFASTTASKSWITCH = 0x0023;
        public const uint SPI_SETFASTTASKSWITCH = 0x0024;
        public const uint SPI_SETDRAGFULLWINDOWS = 0x0025;
        public const uint SPI_GETDRAGFULLWINDOWS = 0x0026;
        public const uint SPI_GETNONCLIENTMETRICS = 0x0029;
        public const uint SPI_SETNONCLIENTMETRICS = 0x002A;
        public const uint SPI_GETMINIMIZEDMETRICS = 0x002B;
        public const uint SPI_SETMINIMIZEDMETRICS = 0x002C;
        public const uint SPI_GETICONMETRICS = 0x002D;
        public const uint SPI_SETICONMETRICS = 0x002E;
        public const uint SPI_SETWORKAREA = 0x002F;
        public const uint SPI_GETWORKAREA = 0x0030;
        public const uint SPI_SETPENWINDOWS = 0x0031;
        public const uint SPI_GETHIGHCONTRAST = 0x0042;
        public const uint SPI_SETHIGHCONTRAST = 0x0043;
        public const uint SPI_GETKEYBOARDPREF = 0x0044;
        public const uint SPI_SETKEYBOARDPREF = 0x0045;
        public const uint SPI_GETSCREENREADER = 0x0046;
        public const uint SPI_SETSCREENREADER = 0x0047;
        public const uint SPI_GETANIMATION = 0x0048;
        public const uint SPI_SETANIMATION = 0x0049;
        public const uint SPI_GETFONTSMOOTHING = 0x004A;
        public const uint SPI_SETFONTSMOOTHING = 0x004B;
        public const uint SPI_SETDRAGWIDTH = 0x004C;
        public const uint SPI_SETDRAGHEIGHT = 0x004D;
        public const uint SPI_SETHANDHELD = 0x004E;
        public const uint SPI_GETLOWPOWERTIMEOUT = 0x004F;
        public const uint SPI_GETPOWEROFFTIMEOUT = 0x0050;
        public const uint SPI_SETLOWPOWERTIMEOUT = 0x0051;
        public const uint SPI_SETPOWEROFFTIMEOUT = 0x0052;
        public const uint SPI_GETLOWPOWERACTIVE = 0x0053;
        public const uint SPI_GETPOWEROFFACTIVE = 0x0054;
        public const uint SPI_SETLOWPOWERACTIVE = 0x0055;
        public const uint SPI_SETPOWEROFFACTIVE = 0x0056;
        public const uint SPI_SETICONS = 0x0058;
        public const uint SPI_GETDEFAULTINPUTLANG = 0x0059;
        public const uint SPI_SETDEFAULTINPUTLANG = 0x005A;
        public const uint SPI_SETLANGTOGGLE = 0x005B;
        public const uint SPI_GETWINDOWSEXTENSION = 0x005C;
        public const uint SPI_SETMOUSETRAILS = 0x005D;
        public const uint SPI_GETMOUSETRAILS = 0x005E;
        public const uint SPI_SCREENSAVERRUNNING = 0x0061;
        public const uint SPI_GETFILTERKEYS = 0x0032;
        public const uint SPI_SETFILTERKEYS = 0x0033;
        public const uint SPI_GETTOGGLEKEYS = 0x0034;
        public const uint SPI_SETTOGGLEKEYS = 0x0035;
        public const uint SPI_GETMOUSEKEYS = 0x0036;
        public const uint SPI_SETMOUSEKEYS = 0x0037;
        public const uint SPI_GETSHOWSOUNDS = 0x0038;
        public const uint SPI_SETSHOWSOUNDS = 0x0039;
        public const uint SPI_GETSTICKYKEYS = 0x003A;
        public const uint SPI_SETSTICKYKEYS = 0x003B;
        public const uint SPI_GETACCESSTIMEOUT = 0x003C;
        public const uint SPI_SETACCESSTIMEOUT = 0x003D;
        public const uint SPI_GETSERIALKEYS = 0x003E;
        public const uint SPI_SETSERIALKEYS = 0x003F;
        public const uint SPI_GETSOUNDSENTRY = 0x0040;
        public const uint SPI_SETSOUNDSENTRY = 0x0041;
        public const uint SPI_GETSNAPTODEFBUTTON = 0x005F;
        public const uint SPI_SETSNAPTODEFBUTTON = 0x0060;
        public const uint SPI_GETMOUSEHOVERWIDTH = 0x0062;
        public const uint SPI_SETMOUSEHOVERWIDTH = 0x0063;
        public const uint SPI_GETMOUSEHOVERHEIGHT = 0x0064;
        public const uint SPI_SETMOUSEHOVERHEIGHT = 0x0065;
        public const uint SPI_GETMOUSEHOVERTIME = 0x0066;
        public const uint SPI_SETMOUSEHOVERTIME = 0x0067;
        public const uint SPI_GETWHEELSCROLLLINES = 0x0068;
        public const uint SPI_SETWHEELSCROLLLINES = 0x0069;
        public const uint SPI_GETMENUSHOWDELAY = 0x006A;
        public const uint SPI_SETMENUSHOWDELAY = 0x006B;
        public const uint SPI_GETSHOWIMEUI = 0x006E;
        public const uint SPI_SETSHOWIMEUI = 0x006F;
        public const uint SPI_GETMOUSESPEED = 0x0070;
        public const uint SPI_SETMOUSESPEED = 0x0071;
        public const uint SPI_GETSCREENSAVERRUNNING = 0x0072;
        public const uint SPI_GETDESKWALLPAPER = 0x0073;
        public const uint SPI_GETACTIVEWINDOWTRACKING = 0x1000;
        public const uint SPI_SETACTIVEWINDOWTRACKING = 0x1001;
        public const uint SPI_GETMENUANIMATION = 0x1002;
        public const uint SPI_SETMENUANIMATION = 0x1003;
        public const uint SPI_GETCOMBOBOXANIMATION = 0x1004;
        public const uint SPI_SETCOMBOBOXANIMATION = 0x1005;
        public const uint SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006;
        public const uint SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007;
        public const uint SPI_GETGRADIENTCAPTIONS = 0x1008;
        public const uint SPI_SETGRADIENTCAPTIONS = 0x1009;
        public const uint SPI_GETKEYBOARDCUES = 0x100A;
        public const uint SPI_SETKEYBOARDCUES = 0x100B;
        public const uint SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES;
        public const uint SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES;
        public const uint SPI_GETACTIVEWNDTRKZORDER = 0x100C;
        public const uint SPI_SETACTIVEWNDTRKZORDER = 0x100D;
        public const uint SPI_GETHOTTRACKING = 0x100E;
        public const uint SPI_SETHOTTRACKING = 0x100F;
        public const uint SPI_GETMENUFADE = 0x1012;
        public const uint SPI_SETMENUFADE = 0x1013;
        public const uint SPI_GETSELECTIONFADE = 0x1014;
        public const uint SPI_SETSELECTIONFADE = 0x1015;
        public const uint SPI_GETTOOLTIPANIMATION = 0x1016;
        public const uint SPI_SETTOOLTIPANIMATION = 0x1017;
        public const uint SPI_GETTOOLTIPFADE = 0x1018;
        public const uint SPI_SETTOOLTIPFADE = 0x1019;
        public const uint SPI_GETCURSORSHADOW = 0x101A;
        public const uint SPI_SETCURSORSHADOW = 0x101B;
        public const uint SPI_GETMOUSESONAR = 0x101C;
        public const uint SPI_SETMOUSESONAR = 0x101D;
        public const uint SPI_GETMOUSECLICKLOCK = 0x101E;
        public const uint SPI_SETMOUSECLICKLOCK = 0x101F;
        public const uint SPI_GETMOUSEVANISH = 0x1020;
        public const uint SPI_SETMOUSEVANISH = 0x1021;
        public const uint SPI_GETFLATMENU = 0x1022;
        public const uint SPI_SETFLATMENU = 0x1023;
        public const uint SPI_GETDROPSHADOW = 0x1024;
        public const uint SPI_SETDROPSHADOW = 0x1025;
        public const uint SPI_GETBLOCKSENDINPUTRESETS = 0x1026;
        public const uint SPI_SETBLOCKSENDINPUTRESETS = 0x1027;
        public const uint SPI_GETUIEFFECTS = 0x103E;
        public const uint SPI_SETUIEFFECTS = 0x103F;
        public const uint SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;
        public const uint SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;
        public const uint SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002;
        public const uint SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003;
        public const uint SPI_GETFOREGROUNDFLASHCOUNT = 0x2004;
        public const uint SPI_SETFOREGROUNDFLASHCOUNT = 0x2005;
        public const uint SPI_GETCARETWIDTH = 0x2006;
        public const uint SPI_SETCARETWIDTH = 0x2007;
        public const uint SPI_GETMOUSECLICKLOCKTIME = 0x2008;
        public const uint SPI_SETMOUSECLICKLOCKTIME = 0x2009;
        public const uint SPI_GETFONTSMOOTHINGTYPE = 0x200A;
        public const uint SPI_SETFONTSMOOTHINGTYPE = 0x200B;
        public const uint SPI_GETFONTSMOOTHINGCONTRAST = 0x200C;
        public const uint SPI_SETFONTSMOOTHINGCONTRAST = 0x200D;
        public const uint SPI_GETFOCUSBORDERWIDTH = 0x200E;
        public const uint SPI_SETFOCUSBORDERWIDTH = 0x200F;
        public const uint SPI_GETFOCUSBORDERHEIGHT = 0x2010;
        public const uint SPI_SETFOCUSBORDERHEIGHT = 0x2011;
        public const uint SPI_GETFONTSMOOTHINGORIENTATION = 0x2012;
        public const uint SPI_SETFONTSMOOTHINGORIENTATION = 0x2013;

        public const uint SC_SCREENSAVE = 0xF140;
        public const uint WM_SYSCOMMAND = 0x0112;

        #endregion

        [DllImport("user32.dll")]
        public static extern int GetClassLong(IntPtr hWnd, long nIndex);



        private const int sICONDIR = 6;            // sizeof(ICONDIR) 
        private const int sICONDIRENTRY = 16;      // sizeof(ICONDIRENTRY)
        private const int sGRPICONDIRENTRY = 14;   // sizeof(GRPICONDIRENTRY)

        /// <summary>
        /// Split an Icon consists of multiple icons into an array of Icon each consist of single icons.
        /// </summary>
        /// <param name="icon">The System.Drawing.Icon to be split.</param>
        /// <returns>An array of System.Drawing.Icon each consist of single icons.</returns>
        public static Icon[] SplitIcon(Icon icon)
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }

            // Get multiple .ico file image.
            byte[] srcBuf = null;
            using (MemoryStream stream = new MemoryStream())
            {
                icon.Save(stream);
                srcBuf = stream.ToArray();
            }

            List<Icon> splitIcons = new List<Icon>();
            {
                int count = BitConverter.ToInt16(srcBuf, 4); // ICONDIR.idCount

                for (int i = 0; i < count; i++)
                {
                    using (MemoryStream destStream = new MemoryStream())
                    using (BinaryWriter writer = new BinaryWriter(destStream))
                    {
                        // Copy ICONDIR and ICONDIRENTRY.
                        writer.Write(srcBuf, 0, sICONDIR - 2);
                        writer.Write((short)1);    // ICONDIR.idCount == 1;

                        writer.Write(srcBuf, sICONDIR + sICONDIRENTRY * i, sICONDIRENTRY - 4);
                        writer.Write(sICONDIR + sICONDIRENTRY);    // ICONDIRENTRY.dwImageOffset = sizeof(ICONDIR) + sizeof(ICONDIRENTRY)

                        // Copy picture and mask data.
                        int imgSize = BitConverter.ToInt32(srcBuf, sICONDIR + sICONDIRENTRY * i + 8);       // ICONDIRENTRY.dwBytesInRes
                        int imgOffset = BitConverter.ToInt32(srcBuf, sICONDIR + sICONDIRENTRY * i + 12);    // ICONDIRENTRY.dwImageOffset
                        writer.Write(srcBuf, imgOffset, imgSize);

                        // Create new icon.
                        destStream.Seek(0, SeekOrigin.Begin);
                        Icon tmp = new Icon(destStream);
                        splitIcons.Add(tmp);
                    }
                }
            }

            return splitIcons.ToArray();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawIconEx(
            IntPtr hdc, // handle to device context
            int xLeft, // x-coord of upper left corner
            int yTop, // y-coord of upper left corner
            IntPtr hIcon, // handle to icon
            int cxWidth, // icon width
            int cyWidth, // icon height
            uint istepIfAniCur, // frame index, animated cursor
            IntPtr dbrFlickerFreeDraw, // handle to background brush
            uint diFlags); // icon-drawing flags


        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hWnd, string str);


        public static void SetOwner(IntPtr hWnd, IntPtr ownerWnd)
        {
            SetWindowLong(hWnd, User32.GWL_HWNDPARENT, (uint)ownerWnd);
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);


        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);


        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);


        [DllImport("user32.dll")]
        public static extern uint SetWindowLong(IntPtr hWnd, long nIndex, uint dwNewLong);

        // The SendMessage function sends the specified message to a 
        // window or windows. It calls the window procedure for the specified 
        // window and does not return until the window procedure
        // has processed the message. 
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            IntPtr hWnd,               // handle to destination window
            int Msg,                // message
            int wParam,             // first message parameter
            [MarshalAs(UnmanagedType.LPStr)] string lParam);
        // second message parameter

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            IntPtr hWnd,               // handle to destination window
            int Msg,                // message
            int wParam,             // first message parameter
            int lParam);            // second message parameter


        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(
          IntPtr hWnd,
          UInt32 msg,
          IntPtr wParam,
          IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern UInt32 SendMessage(
          IntPtr hWnd,
          UInt32 msg,
          UInt32 wParam,
          UInt32 lParam);


        public static void Click(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            //User32.mouse_event( User32.MOUSEEVENTF_ABSOLUTE | User32.MOUSEEVENTF_MOVE , x, y, 0, 0);
            User32.mouse_event(User32.MOUSEEVENTF_ABSOLUTE | User32.MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            System.Threading.Thread.Sleep(20);
            User32.mouse_event(User32.MOUSEEVENTF_ABSOLUTE | User32.MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            System.Threading.Thread.Sleep(100);
        }


        [DllImport("user32.dll")]
        public static extern
          IntPtr FindWindowEx(IntPtr hWnd, IntPtr child, string classe, string window);

        [DllImport("user32.dll")]
        public static extern
          IntPtr FindWindowEx(IntPtr hWnd, int child, string classe, string window);

        [DllImport("user32.dll")]
        public static extern
          IntPtr FindWindow(string classe, string window);

        [DllImport("user32.dll")]
        public static extern
          IntPtr GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);


        [DllImport("user32.dll")]
        public static extern
          IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName,
        int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);


        public static Rectangle GetWindowRect(IntPtr hwnd)
        {
            Rectangle tmp = new Rectangle();
            GetWindowRect(hwnd, ref tmp);
            return new Rectangle(tmp.X, tmp.Y, tmp.Width - tmp.X, tmp.Height - tmp.Y);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //easy Wrapper pour GetClassName (StringBuilder...)
        public static string GetWindowText(IntPtr hWnd)
        {
            try
            {
                System.Text.StringBuilder lpClassName = new System.Text.StringBuilder();
                GetWindowText(hWnd, lpClassName, 65536);
                string className = lpClassName.ToString();
                return className;
            }
            catch
            {
                return "";
            }

        }

        //easy Wrapper pour GetClassName (StringBuilder...)
        public static string GetClassName(IntPtr hWnd)
        {
            try
            {
                System.Text.StringBuilder lpClassName = new System.Text.StringBuilder();
                GetClassName(hWnd, lpClassName, 65536);
                string className = lpClassName.ToString();
                return className;
            }
            catch
            {
                return "";
            }

        }

        //retourne la fenetre firstChild
        public static IntPtr FirstChildWnd(IntPtr root)
        {
            return FindWindowEx(root, new IntPtr(0), null, null);
        }
        //easy wrapper for GetWindowThreadProcessId (out...)
        public static uint GetProcessFromWnd(IntPtr hWnd)
        {
            uint testId;
            GetWindowThreadProcessId(hWnd, out testId);
            return testId;
        }








        private static IntPtr FindWindowRecurse(IntPtr root, string className)
        {

            if (root == IntPtr.Zero
              || GetClassName(root) == className)
                return root; //sortie

            IntPtr next = IntPtr.Zero,
                   testWnd = GetWindow(root, GW_CHILD);

            while (testWnd != IntPtr.Zero)
            {
                next = FindWindowRecurse(testWnd, className);
                if (next != IntPtr.Zero)
                    return next;
                testWnd = GetWindow(testWnd, GW_HWNDNEXT);
            }
            return IntPtr.Zero;
        }

        public static IntPtr GetParentWnd_Class(IntPtr root, string className)
        {
            if (root == IntPtr.Zero
             || GetClassName(root) == className)
                return root; //sortie
            return GetParentWnd_Class(GetParent(root), className);
        }


        public static List<IntPtr> GetWndsFromProcess(uint processId)
        {
            List<IntPtr> result = new List<IntPtr>();

            IntPtr testWnd = FindWindow(null, null), test = IntPtr.Zero;
            do
            {

                if (processId != GetProcessFromWnd(testWnd))
                    continue;

                result.Add(testWnd);
                result.AddRange(GetChildrenWnd(testWnd));

            } while ((testWnd = GetWindow(testWnd, GW_HWNDNEXT)) != IntPtr.Zero);
            return result;
        }



        public static IntPtr FindWindowByPid(uint processId)
        {

            IntPtr testWnd = FindWindow(null, null);
            bool visible;
            IntPtr parent = IntPtr.Zero;

            do
            {
                visible = IsWindowVisible(testWnd);
                parent = GetParent(testWnd);
                if (processId == GetProcessFromWnd(testWnd) && visible && parent == IntPtr.Zero)
                    return testWnd;

            } while ((testWnd = GetWindow(testWnd, GW_HWNDNEXT)) != IntPtr.Zero);

            return IntPtr.Zero;
        }


        public static List<IntPtr> GetChildrenWnd(IntPtr root)
        {
            List<IntPtr> result = new List<IntPtr>();
            IntPtr testWnd = GetWindow(root, GW_CHILD);
            while (testWnd != IntPtr.Zero)
            {
                result.Add(testWnd);
                result.AddRange(GetChildrenWnd(testWnd));
                testWnd = GetWindow(testWnd, GW_HWNDNEXT);
            }
            return result;
        }

        public static Process GetProcessFromWindow(string className)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (GetClassName(p.MainWindowHandle) == className)
                    return p;
            }
            return null;
        }

        public static IntPtr GetWndFromProcess(uint processId, List<IntPtr> root, string className)
        {
            IntPtr test = IntPtr.Zero, testWnd;

            foreach (IntPtr tmp in root)
            {
                testWnd = tmp;
                do
                {
                    //on peut etre parmis les fils de root MAIS avoir un parent...
                    //if (GetParent(testWnd) != IntPtr.Zero)
                    // continue; 

                    if (processId != GetProcessFromWnd(testWnd))
                        continue;


                    test = FindWindowRecurse(testWnd, className);
                    if (test != IntPtr.Zero)
                        return test;

                } while ((testWnd = GetWindow(testWnd, GW_HWNDNEXT)) != IntPtr.Zero);
            }
            return IntPtr.Zero;
        }


        public static IntPtr GetWndFromProcess(uint processId, string className)
        {
            List<IntPtr> roots = new List<IntPtr>();
            roots.Add(FindWindow(null, null));

            return GetWndFromProcess(processId, roots, className);
        }

        public static Rectangle GetWindowsSize(IntPtr hwnd)
        {
            Rectangle tmp = new Rectangle();
            GetWindowRect(hwnd, ref tmp);
            return tmp;
        }

        public static void StartScreenSaver()
        {
            SendMessage(GetDesktopWindow(), WM_SYSCOMMAND, SC_SCREENSAVE, 0);
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);


        public static bool SystemParametersInfo(uint uiAction, bool uiParam, int pvParam, uint fWinIni)
        {
            uint result = 0;
            return SystemParametersInfo(uiAction, (uint)(uiParam ? 1 : 0), ref result, fWinIni);
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);


        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd,
                                              int X,
                                              int Y,
                                              int nWidth,
                                              int nHeight,
                                              bool bRepaint
                                              );


        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr videoWnd);



        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(int dwProcessId);



        // PInvoke declaration for EnumDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        internal static extern int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        // PInvoke declaration for ChangeDisplaySettings Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        internal static extern int ChangeDisplaySettings(ref DEVMODE lpDevMode, int dwFlags);


        public static void ReOrientDisplay(Orientation newOrient)
        {
            DisplaySettings CurrentDisplay = GetDisplaySettings();
            if (newOrient == CurrentDisplay.Orientation)
                return;

            bool newYAxis = newOrient == Orientation.DMDO_DEFAULT || newOrient == Orientation.DMDO_180;
            bool currentYAxis = CurrentDisplay.Orientation == Orientation.DMDO_DEFAULT || CurrentDisplay.Orientation == Orientation.DMDO_180;

            if (newYAxis ^ currentYAxis)
                CurrentDisplay.Size = new Size(CurrentDisplay.Size.Height, CurrentDisplay.Size.Width);

            CurrentDisplay.Orientation = newOrient;

            // switch to new settings
            ChangeDisplaySettings(CurrentDisplay.DEVMODE);
        }

        private static void ChangeDisplaySettings(DEVMODE dm)
        {

            int iRet = ChangeDisplaySettings(ref dm, 0);
            switch (iRet)
            {
                case DISP_CHANGE_SUCCESSFUL:
                    break;
                /*
                case DISP_CHANGE_RESTART:
                  MessageBox.Show("Please restart your system");
                  break;
                case NativeMethods.DISP_CHANGE_FAILED:
                  MessageBox.Show("ChangeDisplaySettigns API failed");
                  break;
                case NativeMethods.DISP_CHANGE_BADDUALVIEW:
                  MessageBox.Show("The settings change was unsuccessful because system is DualView capable.");
                  break;
                case NativeMethods.DISP_CHANGE_BADFLAGS:
                  MessageBox.Show("An invalid set of flags was passed in.");
                  break;
                case NativeMethods.DISP_CHANGE_BADPARAM:
                  MessageBox.Show("An invalid parameter was passed in. This can include an invalid flag or combination of flags.");
                  break;
                case NativeMethods.DISP_CHANGE_NOTUPDATED:
                  MessageBox.Show("Unable to write settings to the registry.");
                  break;
                 * */
                default:
                    throw new Exception("Could save screen informations");
            }
        }



        public struct DisplaySettings
        {
            public Size Size
            {
                get { return new Size(DEVMODE.dmPelsWidth, DEVMODE.dmPelsHeight); }
                set { DEVMODE.dmPelsHeight = value.Height; DEVMODE.dmPelsWidth = value.Width; }
            }
            public int Bits;
            public double Frequency;
            public int Id;

            public Point Position
            {
                get { return new Point(DEVMODE.dmPositionX, DEVMODE.dmPositionY); }
                set { DEVMODE.dmPelsHeight = value.X; DEVMODE.dmPelsWidth = value.Y; }
            }


            public Orientation Orientation
            {
                get { return FlipOrientation[DEVMODE.dmDisplayOrientation]; }
                set { DEVMODE.dmDisplayOrientation = (int)value; }
            }

            internal DEVMODE DEVMODE;
        }



        public static DisplaySettings GetDisplaySettings()
        {
            DEVMODE dm = CreateDevmode();
            GetDisplaySettings(ref dm);
            return new DisplaySettings
            {
                Bits = dm.dmBitsPerPel,
                Frequency = dm.dmDisplayFrequency,
                DEVMODE = dm,
            };

        }

        public static bool MaximizeWindow(string title)
        {
            IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, title);
            return ShowWindow(hwnd, SW_SHOWMAXIMIZED);
        }

        public static bool MinimizeWindow(string title)
        {
            IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, title);
            return ShowWindow(hwnd, SW_MINIMIZE);
        }

        private const int SW_MINIMIZE = 6;


        public static bool HideWindow(uint processId)
        {
            IntPtr hwnd = FindWindowByPid(processId);
            return ShowWindow(hwnd, SW_MINIMIZE);
        }


        public static bool ShowWindow(uint processId)
        {
            IntPtr hwnd = FindWindowByPid(processId);
            return ShowWindow(hwnd, SW_SHOWNORMAL);
        }







        private static int GetDisplaySettings(ref DEVMODE dm)
        {
            // helper to obtain current settings
            return GetDisplaySettings(ref dm, ENUM_CURRENT_SETTINGS);
        }

        private static int GetDisplaySettings(ref DEVMODE dm, int iModeNum)
        {
            // helper to wrap EnumDisplaySettings Win32 API
            return EnumDisplaySettings(null, iModeNum, ref dm);
        }


        // helper for creating an initialized DEVMODE structure
        internal static DEVMODE CreateDevmode()
        {
            DEVMODE dm = new DEVMODE();
            dm.dmDeviceName = new String(new char[32]);
            dm.dmFormName = new String(new char[32]);
            dm.dmSize = (short)Marshal.SizeOf(dm);
            return dm;
        }
    }
}
