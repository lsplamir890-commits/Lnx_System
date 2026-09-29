using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;
using Cosmos.Kernel.System.Vfs;
using Cosmos.Kernel.System.Filesystems.Fat;
using Cosmos.Kernel.HAL.Interfaces.Devices;
using Cosmos.Kernel.HAL.Vfs;
using Cosmos.Kernel.System;
using assembly = System.Reflection.Assembly;
using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Kernel.System.Filesystems.Ext2;
using lnkrnl;
using System.Runtime.InteropServices;
using System.Drawing;
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
        Console.WriteLine(text);       
        Console.ForegroundColor = ConsoleColor.White; 
    }
    public static void warn(string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(text);      
        Console.ForegroundColor = ConsoleColor.White; 
    }
    public static void error(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White; 
    }  
    public static void aprove(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
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