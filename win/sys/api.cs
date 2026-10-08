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
public class Windows
{
   
   
    public bool IsClosed = false;
    public string name {get;set;}
    public int posX = 100;
    public int posY = 100;
    public bool Draggig {get;set;}
    public bool active = true;
    public Color upperbar {get;set;}
    
    public  void Generate(Canvas canvas, int Width, int Height, string style, string WinName, TrueTypeFont mainfont )
    {
        if(this.active == true)
        {
            upperbar = Color.DarkGray;
        }
        else
        {
            upperbar = Color.Gray;
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
                    canvas.DrawString("X", mainfont, Color.White, this.posX, this.posY);
                    canvas.DrawString(this.name, mainfont, Color.White, this.posX + 5,this.posY);
                    while(GuiApi.PressedArea(this.posX,this.posY,Width,37))
                    {
                       
                        this.posX = MouseManager.X;
                        this.posY = MouseManager.Y;
                        return;
                    }
                    
                    if(GuiApi.PressedArea(this.posX,this.posY, 100,100) && this.Draggig == true)
                    {
                        this.Draggig = false;
                        return;
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
    static public bool isClicked = false;
    static public bool PressedArea(int X, int Y, int Width, int Height)
    {
    
        if(MouseManager.LeftButton && !isClicked)
        {
            if(new Rectangle(X,Y,Width,Height).Contains((int)MouseManager.X,(int)MouseManager.Y))
            {
                isClicked = true;
                return true;
            }       
        }
        else if(!MouseManager.LeftButton && isClicked){
             isClicked = false;
        }   
        return false;
    }

}