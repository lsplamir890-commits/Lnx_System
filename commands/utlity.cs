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
using System.Text.Encodings.Web;
namespace lnkrnl;
public class UtilityLNX
{
    public static void Panic(string text, string id)
    {
        try
        {
                    Console.CursorVisible = false;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    txtMgr.error("YOUR PC HAS BEEN HALTED!\n");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    txtMgr.normal("a problem has occurred that caused the system to stop\n\n");
                    txtMgr.normal("For future use, a log file has been made");
                    if(Directory.Exists("/mnt/SysSetts"))
                    {
                    File.AppendAllText("/mnt/SysSetts/Panic.log", $"BSOD at {DateTime.Now}, Name: {text}, ERROR code:{id}\n");
                    } else
                    {

                        Directory.CreateDirectory("/mnt/SysSetts/");
                    }
                    
                    txtMgr.error("ERROR CODE ID:");
                    txtMgr.customNL(ConsoleColor.DarkRed, $"{id}\n\n");
                    txtMgr.error($"ERROR CODE NAME:");
                    
                    txtMgr.customNL(ConsoleColor.DarkRed, $"{text}\n\n");
                    txtMgr.aprove($"IT IS SAFE TO REBOOT[{text},{id}]");
                    
                    Power.Shutdown(); //i use shutdown() as halt btw.
        } 
        catch
        {
            Panic("PANIC_IN_PANIC", "0x0");
        }
       

    }
    public static string ReadIni(string Path, string target)
    {
        string[] file = File.ReadAllLines(Path); 
        foreach(string line in file)
        {
            if(line.Contains(target))
            {
                if(line.Contains("="))
                {
                    string output = line.Split("=")[1];
                    return output;
                }
                
            }
           
            
        }
        return "";
    }
    public static void createfile(string name, string conent)
    {
        string currdir = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currdir, name);
        try
        {
            using FileStream stream = File.Create(filePath);
             File.WriteAllText(filePath, conent);
        } 
        catch
        {
            Console.WriteLine("Failed to create file: " + name);
        }
        
    }
}
