using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WinAPI
{

    
    class Program
    {

    const string CMD_ReOrientDisplay = "ReOrientDisplay";
    const string CMD_GetDisplaySettings = "GetDisplaySettings";
    const string CMD_GetDisplayList = "GetDisplaysList";
    const string CMD_MaximizeWindow = "MaximizeWindow";
    const string CMD_MinimizeWindow = "MinimizeWindow";


        static void Main(string[] args)
        {
            var cmd = args.Length>0 ? args[0] : null;
            if (cmd == CMD_GetDisplaySettings)
            {
                User32.DisplaySettings ta = User32.GetDisplaySettings();
                Console.WriteLine(new JavaScriptSerializer().Serialize(ta));
            }

            if (cmd == CMD_GetDisplayList)
            {

                Console.WriteLine(new JavaScriptSerializer().Serialize(Screen.AllScreens));

            }

            if (cmd == CMD_ReOrientDisplay)
            {
                User32.Orientation orientation = (User32.Orientation)Enum.Parse(typeof(User32.Orientation), args.Length > 1 ? args[1] : null);
                Console.WriteLine(orientation);
                User32.ReOrientDisplay(orientation);
            }

            if (cmd == CMD_MaximizeWindow)
            {
                string title = args[1];
                Console.WriteLine(title);
                User32.MaximizeWindow(title);
            }

            if (cmd == CMD_MinimizeWindow)
            {
                string title = args[1];
                Console.WriteLine(title);
                User32.MinimizeWindow(title);
            }

        }



    }
}
