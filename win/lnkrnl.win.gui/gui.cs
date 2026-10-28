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
using lnkrnl.win.gui;

namespace lnkrnl.win.gui;
public class Desktop
{
    static public Png warn = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.warn.png"));
    static public Png rect = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.rectangle.png"));
    static public Png applogo = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.logo.png"));
    static public Png pfp = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.pfp.png"));
    static public string fontPath = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini","fontPath");
    static public Png cursor = new Png("/mnt/LNXsys/System64/cursor.png");
    static public Png icon = new Png("/mnt/LNXsys/System64/BootIcon.png");
    static public TrueTypeFont fnt = new TrueTypeFont(fontPath, 16);
    
    static public Png deskImg = new Png("/mnt/LNXsys/themes/nebula.png");
    static public string username = UtilityLNX.ReadIni("/mnt/LnxSys/System64/LunDos.ini", "username");
    public void launch(Canvas canvas)
    {
        
        try
        {
        Windows testwindow = new Windows();
        //Windows basicWindow = new Windows();
      
        bool startmenu = false;
        bool testapp = false;
         
        while(true)
        {
            
            canvas.Clear(Color.FromArgb(31,39,71));
            canvas.DrawImage(deskImg, 0, 0);
            canvas.DrawFilledRectangle(Color.FromArgb(255,26,26,26),0, 1039, 129, 41);
            canvas.DrawFilledRectangle(Color.FromArgb(255,26,26,26),1488, 1038, 432, 41);
            canvas.DrawFilledRectangle(Color.FromArgb(255,70,66,66),126, 1039, 3, 42);
            canvas.DrawFilledRectangle(Color.FromArgb(255,70,66,66),1488, 1039, 3, 42);
            
            canvas.DrawString("LNX OS", fnt, Color.FromArgb(255,255,255),21, 9);
            canvas.DrawFilledRectangle(Color.FromArgb(255,89,89,89),1756, 1043, 150, 32);
            canvas.DrawString($"{DateTime.Now}", fnt, Color.FromArgb(255,255,255),1769, 1049);
            canvas.DrawFilledRectangle(Color.FromArgb(255,59,59,59),31, 86, 59, 57);
            canvas.DrawImage(applogo, 29, 86,59,57);
            canvas.DrawString("TEST ICON", fnt, Color.FromArgb(255,255,255),21, 154);
            canvas.DrawFilledRectangle(Color.FromArgb(255,26,26,26),540, 1045, 753, 28);
            canvas.DrawImage(icon, 55, 1049,22,22);
            GuiApi.KeyboardInput(canvas,0,0,100,25,fnt);
            

            if(GuiApi.PressedArea(55, 1049,22,22))
            {
                startmenu = !startmenu;
            }
            if(startmenu)
            {
                canvas.DrawFilledRectangle(Color.FromArgb(255,50,50,50),21, 606, 718, 422);
                canvas.DrawFilledRectangle(Color.FromArgb(255,249,0,0),24, 930, 66, 33);
                canvas.DrawString("quit", fnt, Color.FromArgb(255,255,255),42, 937);
                canvas.DrawFilledRectangle(Color.FromArgb(255,255,234,0),24, 975, 104, 33);
                canvas.DrawString("reboot", fnt, Color.FromArgb(255,255,255),51, 981);
                canvas.DrawString($"{username}", fnt, Color.FromArgb(255,255,255),42, 694);
                canvas.DrawFilledRectangle(Color.FromArgb(255,102,102,102),172, 694, 497, 302);
                canvas.DrawString("APPS:", fnt, Color.FromArgb(255,255,255),188, 707);
                canvas.DrawFilledRectangle(Color.FromArgb(255,59,59,59),244, 758, 59, 57);
                canvas.DrawImage(applogo, 242, 758,59,57);
                canvas.DrawString("TEST ICON", fnt, Color.FromArgb(255,255,255),234, 826);
                canvas.DrawImage(pfp, 43, 621,73,73);
                if(GuiApi.PressedArea(242, 758,59,57))
                {
                    if(testwindow.active == false)
                    {
                           testwindow.active = true; 
                    }
                    testapp = true;
                }


                if(GuiApi.PressedArea(24, 930, 66, 33))
                {
                    //Canvas.DisableFullScreen();
                    Console.Clear();
                    return;
                }
                if(GuiApi.PressedArea(3, 986, 104, 33))
                {
                    Power.Reboot();
                }                
                
                
            }
            if(testapp) testwindow.Generate(canvas,500,500,"nebula","TEST WINDOW",fnt,() =>
                Log.Write("hello")
            );
           
            canvas.DrawImage(cursor, MouseManager.X, MouseManager.Y);
            canvas.DrawString("LNX OS DEVELOPMENT ISO", fnt, Color.FromArgb(255,255,255),1600, 971);
            canvas.DrawString("THIS IS JUST A PRE-RELEASE DESIGN", fnt, Color.FromArgb(255,255,255),1522, 996);
            canvas.Display();
            
        }
        }
        catch(Exception e)
        {
            UtilityLNX.Panic($"GUI_CRASHED_{e}","0xAed0ff");
        }

    }
}