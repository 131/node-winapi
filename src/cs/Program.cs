using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Script.Serialization;

namespace WinAPI
{

    
    class Program
    {

    const string CMD_ReOrientDisplay = "ReOrientDisplay";
    const string  CMD_GetDisplaySettings = "GetDisplaySettings";


        static void Main(string[] args)
        {
            var cmd = args.Length>0 ? args[0] : null;
            if (cmd == CMD_GetDisplaySettings)
            {
                User32.DisplaySettings ta = User32.GetDisplaySettings();
                Console.WriteLine(new JavaScriptSerializer().Serialize(ta));
            }

            if (cmd == CMD_ReOrientDisplay)
            {
                User32.Orientation orientation = (User32.Orientation)Enum.Parse(typeof(User32.Orientation), args.Length > 1 ? args[1] : null);
                Console.WriteLine(orientation);
                User32.ReOrientDisplay(orientation);
            }



        }



    }
}
