using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;


using Cosmos.Kernel.System.FileSystem.Fat;
using Cosmos.Kernel.HAL.DriverKit;
using Cosmos.Kernel.System.Sessions;
using Cosmos.Kernel.System;
using Cosmos.Kernel.HAL.Devices;
using Cosmos.Kernel.System.Diagnostics;

using lnkrnl;
using System.Runtime.InteropServices;
using Cosmos.Executable.Lua;
using Mono.Cecil;
using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using System.IO;
using Cosmos.Kernel.System.FileSystem;
using Cosmos.Kernel.HAL.Devices.Storage;
using Cosmos.Kernel.System.Input.Layouts;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using Cosmos.Kernel.System.Input;
namespace lnkrnl.win.gui;
public class Guinvas
{
    public static Canvas canvas = Canvas.GetFullScreen(InterfaceDevice.target_display_ress);
    public static void init()
    {
        
         MouseManager.SetScreenSize(1920,1080);
        try
        {

           
            while(true)
            {
                Desktop.init(canvas); // initalize the main desktop (windows, startmenu etc)
                //lunwm.init(canvas);
                TaskbarSys.Draw(canvas); // taskbar

                canvas.DrawImage(Desktop.cursor,MouseManager.X,MouseManager.Y); // cursor
                canvas.Display();
            }         
        }
        catch(Exception e)
        {
            UtilityLNX.Panic($"{e.Message}","0x000000");
        }

    }
}