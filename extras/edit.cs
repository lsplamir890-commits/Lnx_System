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
namespace lnkrnl;
public class editor
{
    public static void launch(string path)
    {
        
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal( "FILE OVERWRITER | Type 'eeee' to exit");
        Console.BackgroundColor = ConsoleColor.Black;
        while(true)
        {
            string text = Console.ReadLine();
            switch(text)
            {
                case "eeee":
                Console.Clear();
                return;
                default:
                    File.AppendAllText(path, text + "\n");
                break;
            }
        }
    }   
}