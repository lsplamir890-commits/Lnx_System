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
        Console.WriteLine(text);       
        Console.ForegroundColor = ConsoleColor.White;        
    }
    public static void custom(ConsoleColor colour, string text)
    {
        Console.ForegroundColor = colour;
        Console.Write(text);       
        Console.ForegroundColor = ConsoleColor.White;        
    }    
    public static void info(string text)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[INF] {text}");       
        Console.ForegroundColor = ConsoleColor.White; 
    }
    public static void warn(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine($"[WAR] {text}");         
        Console.ForegroundColor = ConsoleColor.White; 
    }
    public static void error(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine($"[ERR] {text}");    
        Console.ForegroundColor = ConsoleColor.White; 
    }  
    public static void aprove(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine($"[APR] {text}");    
        Console.ForegroundColor = ConsoleColor.White; 
    }  
    public static void normal(string text)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White; 
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