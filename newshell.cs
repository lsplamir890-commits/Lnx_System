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
using System.Runtime.Serialization;
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