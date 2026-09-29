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
public class UtilityLNX
{
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