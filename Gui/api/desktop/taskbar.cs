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
public class TaskbarSys
{
    public static int currentAppX = 55;
    public static bool startmenu = false;
    public static void menu(Canvas startmenu)
    {
        startmenu.DrawImage(Desktop.aero,0, 616,754,430);
        startmenu.DrawFilledRectangle(Color.FromArgb(255,217,217,217),355, 990, 9, 10);
        startmenu.DrawFilledRectangle(Color.FromArgb(255,156,156,156),0, 631, 734, 415);
        startmenu.DrawImage(Desktop.pfp, 47, 631,66,66);
        startmenu.DrawString($"{mainKrn.username}", Desktop.fnt, Color.FromArgb(0,0,0),9, 697);
        startmenu.DrawFilledRectangle(Color.FromArgb(255,197,201,89),617, 1010, 100, 30);
        startmenu.DrawFilledRectangle(Color.FromArgb(255,207,82,82),617, 960, 100, 30);
        startmenu.DrawFilledRectangle(Color.FromArgb(255,226,37,37),617, 910, 100, 30);
        startmenu.DrawString("QUIT", Desktop.fnt, Color.FromArgb(255,255,255),647, 915);
        startmenu.DrawString("SHUTDOWN", Desktop.fnt, Color.FromArgb(218,207,207),619, 965);
        startmenu.DrawString("REBOOT", Desktop.fnt, Color.FromArgb(55,47,47),634, 1016);
        if(GuiApi.PressedArea(617, 910,100,30))
        {
            Power.Shutdown();
        }
        if(GuiApi.PressedArea(617,1010,100,30))
        {
            Power.Reboot();
        }
        if(GuiApi.PressedArea(617,960,100,30))
        {
            mainKrn.launch();
        }       
    }
    public static void Draw(Canvas taskbar)
    {
        try
        {
            taskbar.DrawFilledRectangle(Color.FromArgb(255,22,22,22),0, 1048, 1920, 32);
            taskbar.DrawString("Desktop mode", Desktop.fnt, Color.FromArgb(255,255,255),1549, 1054);
            taskbar.DrawString($"{DateTime.Now} ", Desktop.fnt, Color.FromArgb(255,255,255),1730, 1054);
            taskbar.DrawImage(Desktop.icon, 5, 1051,25,25);
            taskbar.DrawString("LNXOS DEV", Desktop.fnt, Color.FromArgb(255,255,255),73, 36);
            taskbar.DrawImage(Desktop.warn, 16,12,57,57);
            if(startmenu)
            {
                menu(taskbar);
            }   
            if(GuiApi.PressedArea( 5, 1051,25,25))
            {
                startmenu = !startmenu;
            }
            for(int i = 0, x = 55;i != lunwm.appsopen ;i++)
            {
                x += 50;
                taskbar.DrawImage(Desktop.applogo, x, 1049,25,25);
                
            }
                
            

        }
        catch
        {
         UtilityLNX.Panic("lol1", "0x1");
        }
        
    }
}
