using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;

using Cosmos.Network.Telnet;
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
public class lunwm
{
    public static int appsopen = 0;
    public class setts
    {
        
        #region Settings
            //public  Bitmap data {get;set;}
            //public static Bitmap test = new Bitmap(1920, 1080,[0xff], ColorDepth.ColorDepth32)
            //public   Stream state;
            public bool open = true;
            public bool active = true;
            public bool marked = false;
            public int width {get;set;}
            public int height {get;set;}
            public bool isDragging = false; 
            public int x = 100;
            public int y = 100;
        #endregion

    }



    //Be carefull with this code, is the low level 
    //window manager
    #region WINDOW MANAGER
        public  setts winSetts = new setts();
        // public  void init(Canvas c)
        // {
            
        //     //winSetts.data = new Bitmap(1920, 1080, EmbeddedResource.ReadBytes("lnkrnl.resources.rectangle.png") ,ColorDepth.ColorDepth32);
        //     c.DrawString("#REGION WINDOW MANAGER IS INIT", Desktop.fnt,Color.White,0,0);
        // }
        public void Generate(Png c,int width, int height, string name, bool open = false)
        { 
            if(!winSetts.open && !open)
            {
                return;
            }
            if(!winSetts.marked)
            {
                winSetts.marked = true;
                appsopen += 1;
            } 
            
            if (winSetts.isDragging == true)
            {

                winSetts.x = MouseManager.X - (width / 2);
                winSetts.y = MouseManager.Y - 10;
                if (!MouseManager.LeftButton)
                {
                    winSetts.isDragging = false;
                }
            }
            //generate the actual WINDOW data, forexample: lunwm.Generate(500,500,"test");
            Guinvas.canvas.DrawImage(c,winSetts.x,winSetts.y,width,height);
            Guinvas.canvas.DrawString(name, Desktop.fnt,Color.White,winSetts.x,winSetts.y);
            // Guinvas.canvas.DrawFilledRectangle(color,x,y,width,height);
            if(GuiApi.PressedArea(winSetts.x,winSetts.y,width,20))
            {
                winSetts.isDragging = true;

                return;
            }
            
        }
        //public static void DrawSys()
    #endregion 
}