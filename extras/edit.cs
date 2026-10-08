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
public class editor
{
    public static void launch(string path)
    {
        
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Gray;
        txtMgr.normal( "FILE EDITOR | Press enter and Type 'eeee' to exit");
        Console.BackgroundColor = ConsoleColor.Black;
        if(File.Exists(path))
        {
            txtMgr.normal(File.ReadAllText(path));
        }
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
