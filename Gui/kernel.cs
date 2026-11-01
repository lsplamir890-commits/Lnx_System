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
public class InterfaceDevice
{
    public static Mode target_display_ress = new Mode(1920,1080,ColorDepth.ColorDepth32);
    public static Mode static_display_ress = new Mode(1024,768,ColorDepth.ColorDepth32);
    public static int InitDesktopDriver()
    {
        txtMgr.info("Now initializing Desktop drivers...");
        if(InterfaceDevice.GetModes())
        {
            Canvas canvas = Canvas.GetFullScreen(target_display_ress);
            canvas.Clear(Color.Black);
            txtMgr.aprove($"Display drivers are initialized as:{target_display_ress.Width}x{target_display_ress.Height}");
            Console.Clear();
        }
        else
        {
            Canvas canvas = Canvas.GetFullScreen(static_display_ress);
            canvas.Clear(Color.Black);
            txtMgr.warn($"Display drivers are initialized as:{static_display_ress.Width}x{static_display_ress.Height}");
            Console.Clear();
        }
        return 0;
    }
    public static bool GetModes()
    {
        IReadOnlyList<Mode> modes = Canvas.GetFullScreen().AvailableModes;
        foreach(Mode mode in modes)
        {

            txtMgr.info($"Found mode found using \"GetModes()\": {mode.Width}x{mode.Height} (Extra info: {mode.ColorDepth}) ");
            if(mode.Width == target_display_ress.Width && mode.Height == target_display_ress.Height && mode.ColorDepth == target_display_ress.ColorDepth)
            {
                txtMgr.aprove($"{mode.Width}x{mode.Height} (Extra info: {mode.ColorDepth}) Is supported for LNXOS HD");
                return true;
            }
        }
        txtMgr.error("LnxOS didnt encounter any mode related to 1920x1080. Any fallback code will now run");
        return false;
    }
}