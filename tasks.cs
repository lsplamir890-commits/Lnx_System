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

public class taskMgr
{
    public static int taskIds = 0;
    public static Dictionary<string, int> tasks = new Dictionary<string, int>();
    public static void init(string name)
    {
        
        if(!tasks.TryAdd(name, taskIds))
        {
            txtMgr.error("failed to make task #" + taskIds);
            return;
        }
        Log.WriteString($"started procces {name}\n");

        
        taskIds += 1;
    }
    public static void List()
    {
        foreach(KeyValuePair<string,int> ls in tasks)
        {
            txtMgr.custom(ConsoleColor.White,$"Name: {ls.Key} ID: {ls.Value}\n");
        }
    }
    public static void end(string name)
    {
        if(tasks.ContainsKey(name))
        {
            
            tasks.Remove(name);
        }

        else
        {
            txtMgr.error($"Failed to remove task {name} because it doesnt exist");
        }
    }

}