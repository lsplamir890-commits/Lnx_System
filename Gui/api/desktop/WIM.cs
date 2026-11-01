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
public class Window
{
    public static void Draw()
    {
        Wim test = new Wim();
        try
        {
            test.show(500,500,"test window");
        }
        catch
        {
            UtilityLNX.Panic("lol2","0x0");
        }

        //return;
    }
    public class Wim
    {
        public Bitmap range {get;set;}
        public bool closed {get;set;}
        public bool moving {get;set;}
        public int x = 100;
        public int y = 100;
        public void show(int width, int height, string name)
        {
            range = new Bitmap(width,height, ColorDepth.ColorDepth32);
            if(moving == true)
            {
                x = MouseManager.X - (width / 2);
                y = MouseManager.Y - 20;
                if(!MouseManager.LeftButton)
                {
                    moving = false;
                }
            }
            //ImprovedVBE.(range,x,y,width,height,range);
            ImprovedVBE.DrawFilledRectangle(range,ImprovedVBE.colourToNumber(44,44,44),x,y,width,height,true);
            //Guinvas.canvas.DrawString(name, Desktop.fnt,Color.White,x + 10, y + 10);
            if(GuiApi.PressedArea(x,y,width,40))
            {
                moving = true;
                return;
            }
        }
    }

}