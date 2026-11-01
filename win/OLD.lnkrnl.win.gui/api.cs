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
    public string name { get; set; }
    public int posX = 100;
    public int posY = 100;
    public bool Draggig { get; set; }
    public bool active = true;
    public Color upperbar { get; set; }

    public void Generate(Canvas canvas, int Width, int Height, string style, string WinName, TrueTypeFont mainfont, Action action)
    {
    
        if (IsClosed)
        {
            return;
        }
        this.name = WinName;
        Log.WriteString("generated window");
        if (this.Draggig == true)
        {

            posX = MouseManager.X - (Width / 2);
            posY = MouseManager.Y - 10;
            if (!MouseManager.LeftButton)
            {
                Draggig = false;
            }
        }
        switch (style.ToLower())
        {
            case "nebula":
                canvas.DrawFilledRectangle(Color.FromArgb(54, 58, 79), posX, posY, Width, Height);
                canvas.DrawFilledRectangle(Color.FromArgb(60, 24, 25, 38), posX, posY, Width, 31);
                canvas.DrawString(this.name, mainfont, Color.White, posX + 22, posY);
                canvas.DrawFilledRectangle(Color.DarkRed, posX, posY, 22, 31);
                canvas.DrawString("X", mainfont, 28, Color.White, posX, posY);
                action();
                if (GuiApi.PressedArea(posX, posY, 20, 31))
                {
                    IsClosed = true;
                    return;
                }
                if (GuiApi.PressedArea(posX, posY, Width, 31))
                {
                    this.Draggig = true;
                }

                return;

            case "basic":
                canvas.DrawFilledRectangle(Color.DarkGray, posX, posY, Width, Height);
                canvas.DrawFilledRectangle(Color.Gray, posX, posY, Width, 31);
                canvas.DrawString(this.name, mainfont, Color.White, posX + 22, posY);
                canvas.DrawFilledRectangle(Color.Red, posX, posY, 22, 31);
                canvas.DrawString("X", mainfont, 28, Color.White, posX, posY);
                if (GuiApi.PressedArea(posX, posY, 20, 31))
                {
                    IsClosed = true;
                    return;
                }
                if (GuiApi.PressedArea(posX, posY, Width, 31))
                {
                    this.Draggig = true;
                }

                break;
            default:
                //if you are wondering whats the default theme. is basiccly the basic theme
                canvas.DrawFilledRectangle(Color.DarkGray, posX, posY, Width, Height);
                canvas.DrawFilledRectangle(Color.Gray, posX, posY, Width, 31);
                canvas.DrawString(this.name, mainfont, Color.White, posX + 22, posY);
                canvas.DrawFilledRectangle(Color.Red, posX, posY, 22, 31);
                canvas.DrawString("X", mainfont, 28, Color.White, posX, posY);
                if (GuiApi.PressedArea(posX, posY, 20, 31))
                {
                    IsClosed = true;
                    return;
                }
                if (GuiApi.PressedArea(posX, posY, Width, 31))
                {
                    this.Draggig = true;
                }
                break;

        }
        if (!GuiApi.PressedArea(posX, posY, Width, Height))
        {
            active = false;
        }
    }
}
public class GuiApi
{
    static public bool isClicked = false;
    static public string text;
    static public bool inputOpened = false;

    static public string KeyboardInput(Canvas canvas, int x, int y, int Width, int height, TrueTypeFont font)
    {
        canvas.DrawFilledRectangle(Color.Gray, x, y, Width, height);
        canvas.DrawString(text, font, Color.White, x, y);
        if(inputOpened == true)
        {
            if(GuiApi.NOTPressedArea(x,y,Width,height))
            {
                inputOpened = false;
            }
            if (KeyboardManager.TryReadKey(out KeyEvent? key))
            {
                text += key.Key;
            }
            return text;
        }
        if (PressedArea(x, y, Width, height))
        {
            inputOpened = true;

        }
        return "";
    }
    static public bool Hovering(int X, int Y, int Width, int Height)
    {
        if (new Rectangle(X, Y, Width, Height).Contains(MouseManager.X, MouseManager.Y))
        {
            return true;
        }
        return false;
    }
    static public bool NOTPressedArea(int X, int Y, int Width, int Height)
    {

        
        if (MouseManager.LeftButton && !new Rectangle(X, Y, Width, Height).Contains((int)MouseManager.X, (int)MouseManager.Y))
        {
            return true;
        }
        return false;
    }
    static public bool PressedArea(int X, int Y, int Width, int Height)
    {

        if (MouseManager.LeftButton && !isClicked)
        {
            if (new Rectangle(X, Y, Width, Height).Contains((int)MouseManager.X, (int)MouseManager.Y))
            {
                isClicked = true;
                return true;
            }
        }
        else if (!MouseManager.LeftButton && isClicked)
        {
            isClicked = false;
        }
        return false;
    }

}