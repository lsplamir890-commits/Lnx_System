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

namespace lnkrnl;
public class Desktop
{
    static public string fontPath = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini","fontPath");
    static public Png cursor = new Png("/mnt/LNXsys/System64/cursor.png");
    static public Png icon = new Png("/mnt/LNXsys/System64/BootIcon.png");
    static public TrueTypeFont regularfont = new TrueTypeFont(fontPath,31);
    
    static public Png deskImg = new Png("/mnt/LNXsys/themes/nebula.png");
    static public string username = UtilityLNX.ReadIni("/mnt/LnxSys/System64/LunDos.ini", "username");
    public void launch()
    {
        try
        {
        //                 Windows testwindow = new Windows();
        
        Canvas canvas = Canvas.GetFullScreen(new Mode(1920, 1080, ColorDepth.ColorDepth32));
          
        MouseManager.SetScreenSize(canvas.Width, canvas.Height);
        bool startmenu = false;
         
        while(true)
        {
            
            canvas.Clear(Color.FromArgb(0,0,0));
            canvas.DrawImage(deskImg, 0,0);

            canvas.DrawFilledRectangle(Color.FromArgb(255,34,34,34),0, 1043, 1920, 37);
            canvas.DrawString($"{DateTime.Now}", regularfont, Color.FromArgb(255,255,255),1558, 1046);
            
            canvas.DrawString($"Build: {UtilityLNX.build} ", regularfont, Color.FromArgb(255,255,255),1512, 0);
            canvas.DrawString($"Running from directory: {Directory.GetCurrentDirectory()} ", regularfont, Color.FromArgb(255,255,255),0, 0);
            
            canvas.DrawFilledRectangle(Color.FromArgb(255,70,66,66),92, 1043, 3, 38);
            canvas.DrawFilledRectangle(Color.FromArgb(255,70,66,66),1488, 1043, 3, 38);
            //testwindow.Generate(canvas, 400,400,"dadasd","test",regularfont);

            

            


            canvas.DrawFilledRectangle(Color.FromArgb(0,50,157,35),41, 1052, 21, 19); //startmenu button
            //canvas.DrawImage(icon,41,1052,21,29,0);
            //canvas.DrawImage(icon, 14, 1056);
            if(GuiApi.PressedArea(41, 1052, 21, 19))
            {
                startmenu = !startmenu;
            }
            if(startmenu)
            {
                canvas.DrawFilledRectangle(Color.FromArgb(255,108,104,104),102, 669, 414, 353);
                canvas.DrawFilledRectangle(Color.FromArgb(255,249,0,0),362, 938, 66, 33);
                canvas.DrawString($"quit", regularfont, Color.FromArgb(255,255,255),362, 935);
                canvas.DrawFilledRectangle(Color.FromArgb(255,228,215,76),362, 981, 104, 33);
                canvas.DrawString($"reboot", regularfont, Color.FromArgb(255,255,255),362, 978);
                canvas.DrawString($"{username}", regularfont, Color.FromArgb(255,255,255),207, 678);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),508, 693, 24, 24);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),109, 693, 24, 24);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),508, 998, 24, 24);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),109, 998, 24, 24);
                canvas.DrawFilledRectangle(Color.FromArgb(255,108,104,104),516, 690, 16, 306);
                canvas.DrawFilledRectangle(Color.FromArgb(255,108,104,104),85, 690, 18, 306);
                    

                if(GuiApi.PressedArea(362, 938, 66, 33))
                {
                    Canvas.DisableFullScreen();
                    Console.Clear();
                    return;
                }
                if(GuiApi.PressedArea(362, 981, 104, 33))
                {
                    Power.Reboot();
                }                
                
                
            }
           
           
            canvas.DrawImage(cursor, MouseManager.X, MouseManager.Y);

            canvas.Display();
            
        }
        }
        catch(Exception e)
        {
            UtilityLNX.Panic($"GUI_CRASHED_{e}","0xAed0ff");
        }

    }
}