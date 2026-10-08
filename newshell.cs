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
public class Newshell
{
    
    

    public static void Launch()
    {
        Console.Clear();
        txtMgr.info("[OK] booted succesfully");
        txtMgr.aprove("Shell Revamp LNX OS V0.7");
        CmdMgr.Init();
        while(true)
        {
            Console.Write($"@~ $ {Directory.GetCurrentDirectory()} > ");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();

            string[] parts = input.Split(' ');
            string commandName = parts[0];

            string[] args = new string[parts.Length - 1];
            Array.Copy(parts, 1, args, 0, args.Length);
            if(CmdMgr.RegComms.ContainsKey(commandName))
            {
                CmdMgr.RegComms[commandName](args);
            }
            else
            {
                txtMgr.error($"Could not find {commandName} as a command");
            }
            
        }
    }

}