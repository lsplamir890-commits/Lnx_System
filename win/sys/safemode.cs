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
public class SafeKrn
{
    static public void launch()
    {
        
        txtMgr.aprove("SAFE MODE LAUNCHED SUCCESFULLY");
        txtMgr.error("No disk support avaible because of safe mode");
        txtMgr.warn("SAFE MODE IS ONLY GOOD FOR PC'S THAT DONT HAVE NVME OR ACHI");
        CliShell();
    } 
    static public void CliShell()
    {
        while(true)
        {
            txtMgr.custom(ConsoleColor.Magenta, $"SAFEMODE@:~ $ ");
            string? input = Console.ReadLine().ToLower();
            switch(input)
            {
                case "help":
                    txtMgr.normal("Available commands:");
                    txtMgr.normal("  Help       - Shows this text ");
                    txtMgr.normal("  Clear      - Clears the screen ");
                    txtMgr.normal("  Halt       - Halts the computer");
                    txtMgr.normal("  Crash      - Force's a crash");
                    txtMgr.normal("  Tasks      - Shows every process running");
                    txtMgr.normal("  Lua        - Launches a lua envoriment");
                break;
                case "clear":
                    Console.Clear();      
                break;
                case "halt":
                    UtilityLNX.halt();
                break;
                case "crash":
                    UtilityLNX.SafePanic("CRASH_ON_PURPOSE", "0x00000");
                break;
                case "tasks":
                    taskMgr.List();
                break;
                case "lua":
                    Lua.Environment();
                break;
                default:
                    txtMgr.error($"\"{input}\" is not a command");
                break;
            }
        } 
        
    }
}