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
public class GuiData
{
    public static Mode target = new Mode(1920,1080,ColorDepth.ColorDepth32);
    public void IfSupportedInitGui()
    {
        bool supported = HDIsSupported();

        if(supported == true)
        {
            Canvas SetCanvasDriver = Canvas.GetFullScreen(target);
            MouseManager.SetScreenSize(SetCanvasDriver.Width, SetCanvasDriver.Height);
            MouseManager.SetPosition(1920/2,1080/2);
            Desktop desktop = new Desktop();
            desktop.launch(SetCanvasDriver);
            return;
        }
       
        txtMgr.error("GUI IS NOT SUPPORTED ON THE MODES SHOWN");
    }
    public static bool HDIsSupported()
    {
        IReadOnlyList<Mode> modes = Canvas.GetFullScreen().AvailableModes;
        foreach(Mode mode in modes)
        {
            txtMgr.info($"Gui Mode is supported:{mode.Width}x{mode.Height}, {mode.ColorDepth}");
            if(mode.Width == target.Width && mode.Height == target.Height && mode.ColorDepth == target.ColorDepth)
            {
                return true;
            }
        }
        return false;
    }
}