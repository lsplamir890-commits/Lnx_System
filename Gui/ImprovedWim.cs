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
    public class window
    {
     public window(int width, int height,string name, int x = 100,int y = 100, bool active = true)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.name = name;
        this.active = active;
    }
        #region Settings

            //public  Bitmap data {get;set;}
            //public static Bitmap test = new Bitmap(1920, 1080,[0xff], ColorDepth.ColorDepth32)
            //public   Stream state;
            public string name {get;set;}
            public bool open = true;
            public bool active  {get;set;}
            public bool marked = false;
            public int width {get;set;}
            public int height {get;set;}
            public bool isDragging = false; 
            public int x = 100;//place holder
            public int y = 100;//place holder
        #endregion

    }
public class lunwm
{
    public  window winSetts;
    public static  List<window> windows;
    public static int appsopen = 0;

    #region PRE WINDOW MANAGER
    public lunwm(string name,int windowWidth, int windowHeight, int x = 100, int y = 100)
    {
        windows = new List<window>();
        winSetts  = new window(windowWidth,windowHeight,name,x,y, true);
        winSetts.width = windowWidth;
        winSetts.height = windowHeight;
    }
    
    #endregion
    public bool ActiveCheck()
    {
        foreach(window win in windows)
        {
            if(winSetts.active == true)
            {
                win.active = false;
                return true;
            }

        }
        return false;
    }

    //Be carefull with this code, is the low level 
    //window manager
    #region WINDOW MANAGER
        
        // public  void init(Canvas c)
        // {
            
        //     //winSetts.data = new Bitmap(1920, 1080, EmbeddedResource.ReadBytes("lnkrnl.resources.rectangle.png") ,ColorDepth.ColorDepth32);
        //     c.DrawString("#REGION WINDOW MANAGER IS INIT", Desktop.fnt,Color.White,0,0);
        // }
        public void regWin()
        {
            windows.Add(winSetts);
        }
        public void RemoveWin()
        {
            windows.Remove(winSetts);
        }
        public void draw(Png c)
        { 
            Color colorText = winSetts.active ? Color.White : Color.Black;
            if(!winSetts.marked)
            {
                winSetts.marked = true;
                appsopen += 1;
            } 
            
            if (winSetts.isDragging == true)
            {

                winSetts.x = MouseManager.X - (winSetts.width / 2);
                winSetts.y = MouseManager.Y - 10;
                if (!MouseManager.LeftButton)
                {
                    winSetts.isDragging = false;
                }
            }
            //generate the actual WINDOW data, forexample: lunwm.Generate(500,500,"test");
            Guinvas.canvas.DrawImage(c,winSetts.x,winSetts.y,winSetts.width,winSetts.height);
            Guinvas.canvas.DrawString(winSetts.name, Desktop.fnt,colorText,winSetts.x,winSetts.y);
            // Guinvas.canvas.DrawFilledRectangle(color,x,y,width,height);
            if(GuiApi.PressedArea(winSetts.x,winSetts.y,winSetts.width,20))
            {
                winSetts.isDragging = true;

                return;
            }
            
        }
        //public static void DrawSys()
    #endregion 
}
