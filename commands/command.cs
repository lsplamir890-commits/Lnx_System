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
public class txtMgr
{

    public static void customNL(ConsoleColor colour, string text)
    {
        Console.ForegroundColor = colour;
        Console.Write($"");         
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine(text);    
    }
    public static void custom(ConsoleColor colour, string text)
    {
        Console.ForegroundColor = colour;
        Console.Write($"")         ;
        Console.ForegroundColor = ConsoleColor.White; 
        Console.Write(text);     
    }    
    public static void info(string text)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"[ INF ] ");         
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine(text);
    }
    public static void warn(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"[ WAR ] ");         
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine(text);
    }
    public static void error(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[ ERR ] ");         
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine(text);
    }  
    public static void aprove(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[ SUC ] ");         
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine(text);
    }  
    public static void normal(string text)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White; 
    } 
}
public static class LN_Message_Box
{
    public static void InfoMessageBox(string title, string text)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();
        Console.Beep();
        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal($"===={title}====");
        Console.BackgroundColor = ConsoleColor.Blue;
        txtMgr.normal("INFORMATION!:");
        txtMgr.normal(text);


        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal($"    OK    ");
        Console.BackgroundColor = ConsoleColor.Blue;
        if(Console.ReadKey(true).Key == ConsoleKey.Enter)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return;
        }
    }
    public static void ErrorMessageBox(string title, string text)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Clear();
        Console.Beep();
        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal($"===={title}====");
        Console.BackgroundColor = ConsoleColor.Yellow;
        txtMgr.normal("INFORMATION!:");
        txtMgr.normal(text);


        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal($"    OK    ");
        Console.BackgroundColor = ConsoleColor.Yellow;
        if(Console.ReadKey(true).Key == ConsoleKey.Enter)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return;
        }
    }
    public static void WarnMessageBox(string title, string text)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Clear();
        Console.Beep();
        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal($"===={title}====");
        Console.BackgroundColor = ConsoleColor.Yellow;
        txtMgr.normal("WARNING!:");
        txtMgr.normal(text);


        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal($"    OK    ");
        Console.BackgroundColor = ConsoleColor.Yellow;
        if(Console.ReadKey(true).Key == ConsoleKey.Enter)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return;
        }
    }
}
public class CmdMgr
{
    public static Dictionary<string, Action<string[]>> RegComms = new Dictionary<string, Action<string[]>>();
    public static void Init()
    {
        
        RegComms.Add("abt", args => txtMgr.info("this is a beta."));
    }
    
}