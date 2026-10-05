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
using Cosmos.Kernel.System.Mouse;
using System.Drawing;
using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
namespace lnkrnl;
public class Windows
{
   
    public bool IsClosed = false;
    public string name {get;set;}
    public int posX = 100;
    public int posY = 100;
    public bool Draggig {get;set;}
    public bool active = true;
    public Color upperbar {get;set;}
    
    public  void Generate(Canvas canvas, int Width, int Height, string style, string WinName )
    {
    
        if(this.active == true)
        {
            upperbar = Color.Gray;
        }
        else
        {
            upperbar = Color.DarkGray;
        }
        this.name = WinName;
        
       
     
        if (GuiApi.PressedArea(this.posX, this.posY, Width, Height))
        {
            this.active = true;

        }
        else
        {
            this.active = false;
        }
        if(this.IsClosed == false)
        {
            switch(style)
            {
                case"basicTheme":
                break;
                default:
                    canvas.DrawFilledRectangle(Color.White, this.posX, this.posY, Width, Height); //base, everthing should go with it
                    canvas.DrawFilledRectangle(upperbar, this.posX,this.posY,Width,37);
                    canvas.DrawRectangle(Color.Red, this.posX, this.posY, 5,35);
                    canvas.DrawString("X", PCScreenFont.DefaultFont, Color.White, this.posX, this.posY);
                    canvas.DrawString(this.name, PCScreenFont.DefaultFont, Color.White, this.posX + 5,this.posY);
                    if(GuiApi.PressedArea(this.posX,this.posY,Width,37))
                    {
                       
                        this.Draggig = true;
                    }
                    if(GuiApi.PressedArea(this.posX,this.posY,5,35))
                    {
                        this.IsClosed = true;
                    }
                    if(GuiApi.PressedArea(this.posX,this.posY, 100,100) && this.Draggig == true)
                    {
                        this.Draggig = false;
                    }
                break;
            }
        }
        if(this.Draggig)
        {
            this.posX = MouseManager.X;
            this.posY = MouseManager.Y;
        }
        
    }
}
public class GuiApi
{


    static public bool PressedArea(int X, int Y, int Width, int Height)
    {
    
        if(MouseManager.LeftButton)
        {
            if(new Rectangle(X,Y,Width,Height).Contains((int)MouseManager.X,(int)MouseManager.Y))
            {
                Thread.Sleep(100);
                return true;
            }       
        }
        return false;
    }

}