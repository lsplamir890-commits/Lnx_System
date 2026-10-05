using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;
using Cosmos.Kernel.System.Vfs;
using Cosmos.Kernel.System.Filesystems.Fat;
using Cosmos.Kernel.HAL.Interfaces.Devices;
using Cosmos.Kernel.HAL.Vfs;
using Cosmos.Kernel.System;

using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Kernel.System.Filesystems.Ext2;
using lnkrnl;
using System.Runtime.InteropServices;
using Cosmos.Kernel.System.Graphics;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;

using Cosmos.Kernel.System.Graphics.Fonts;
using Cosmos.Kernel.System.Mouse;

namespace lnkrnl;
public class Desktop
{
    static public Png cursor = new Png("/mnt/LNXsys/System64/cursor.png");
    static public Png icon = new Png("/mnt/LNXsys/System64/BootIcon.png");
    static public Png deskImg = new Png("/mnt/LNXsys/themes/nebula.png");
    static public string username = UtilityLNX.ReadIni("/mnt/LnxSys/System64/LunDos.ini", "username");
    public void launch()
    {
         Windows testwindow = new Windows();
         Windows testwindow2 = new Windows();
        Canvas canvas = Canvas.GetFullScreen(new Mode(1920, 1080, ColorDepth.ColorDepth32));
          
        MouseManager.SetScreenSize(canvas.Width, canvas.Height);
        bool startmenu = false;
         
        while(true)
        {
            
            canvas.Clear(Color.FromArgb(0,0,0));
            canvas.DrawImage(deskImg, 0,0);

            canvas.DrawFilledRectangle(Color.FromArgb(255,34,34,34),0, 1043, 1920, 37);
            canvas.DrawString($"{DateTime.Now}", PCScreenFont.DefaultFont, Color.FromArgb(255,255,255),1558, 1046);
            
            canvas.DrawString($"Build: {UtilityLNX.build} ", PCScreenFont.DefaultFont, Color.FromArgb(255,255,255),1512, 0);
            canvas.DrawString($"Running from directory: {Directory.GetCurrentDirectory()} ", PCScreenFont.DefaultFont, Color.FromArgb(255,255,255),0, 0);
            
            canvas.DrawFilledRectangle(Color.FromArgb(255,70,66,66),92, 1043, 3, 38);
            canvas.DrawFilledRectangle(Color.FromArgb(255,70,66,66),1488, 1043, 3, 38);
            testwindow.Generate(canvas, 400,400,"dadasd","test");

            

            


            canvas.DrawFilledRectangle(Color.FromArgb(255,50,157,35),41, 1052, 21, 19); //startmenu button
            canvas.DrawImageAlpha(icon, 14, 1056);
            if(GuiApi.PressedArea(41, 1052, 21, 19))
            {
                startmenu = !startmenu;
            }
            if(startmenu)
            {
                canvas.DrawFilledRectangle(Color.FromArgb(255,108,104,104),102, 669, 414, 353);
                canvas.DrawFilledRectangle(Color.FromArgb(255,249,0,0),362, 938, 66, 33);
                canvas.DrawString($"quit", PCScreenFont.DefaultFont, Color.FromArgb(255,255,255),362, 935);
                canvas.DrawFilledRectangle(Color.FromArgb(255,228,215,76),362, 981, 104, 33);
                canvas.DrawString($"reboot", PCScreenFont.DefaultFont, Color.FromArgb(255,255,255),362, 978);
                canvas.DrawString($"{username}", PCScreenFont.DefaultFont, Color.FromArgb(255,255,255),207, 678);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),508, 693, 24, 24);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),109, 693, 24, 24);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),508, 998, 24, 24);
                canvas.DrawFilledEllipse(Color.FromArgb(255,108,104,104),109, 998, 24, 24);
                canvas.DrawFilledRectangle(Color.FromArgb(255,108,104,104),516, 690, 16, 306);
                canvas.DrawFilledRectangle(Color.FromArgb(255,108,104,104),85, 690, 18, 306);
                    

                if(GuiApi.PressedArea(362, 938, 66, 33))
                {
                    Console.Clear();
                    return;
                }
                if(GuiApi.PressedArea(362, 981, 104, 33))
                {
                    Power.Reboot();
                }                
                
                
            }
           
           
            canvas.DrawImageAlpha(cursor, MouseManager.X, MouseManager.Y);

            canvas.Display();
            GC.Collect();
            
        }
    }
}