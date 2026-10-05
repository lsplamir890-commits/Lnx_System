using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;
using Cosmos.Kernel.System.Vfs;
using Cosmos.Kernel.System.Filesystems.Fat;
using Cosmos.Kernel.HAL.Interfaces.Devices;
using Cosmos.Kernel.HAL.Vfs;
using Cosmos.Kernel.System;
using  System.Reflection;
using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Kernel.System.Filesystems.Ext2;
using lnkrnl;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using Cosmos.Executable.Lua;
namespace lnkrnl;
    public static class EmbeddedResource
    {
        public static string ReadString(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resources = assembly.GetManifestResourceNames();

            foreach (string resource in resources)
            {
                if (!resource.EndsWith(fileName, StringComparison.OrdinalIgnoreCase)) continue;

                using Stream? stream = assembly.GetManifestResourceStream(resource);

                if (stream == null) throw new Exception("Failed to open embedded resource: " + resource);

                using StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }

            throw new Exception("Embedded resource not found: " + fileName);
        }

        public static byte[] ReadBytes(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resources = assembly.GetManifestResourceNames();

            foreach (string resource in resources)
            {
                if (!resource.EndsWith(fileName, StringComparison.OrdinalIgnoreCase))
                    continue;

                using Stream? stream = assembly.GetManifestResourceStream(resource);

                if (stream == null)
                    throw new Exception(
                        "Failed to open embedded resource: " + resource);

                using MemoryStream memoryStream = new MemoryStream();

                stream.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }

            throw new Exception(
                "Embedded resource not found: " + fileName);
        }
    }
public class UtilityLNX
{
    static public int build = 07;
    static  bool panic_inpanic = false;
    public static void luaenvoriment()
    {
            try
            {
             
                Console.Clear();
                LuaInterpreter lua = new()
                {
                    WorkingDirectory = $"{Directory.GetCurrentDirectory()}"  
                };
        
                txtMgr.info($"lua 5.2.0 interpreter\nWorking directory:{Directory.GetCurrentDirectory()}");
                lua.RunPrompt();
               
            }
            catch (LuaException e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine(e.LuaStackTrace);
            }
            return;
    }
    public static void createpart()
    {
         IBlockDevice? disk = StorageManager.PrimaryDevice;
        Gpt.Create(disk);
        if (!PartitionManager.Create(disk, startSector: 2048, sectorCount: 131072,
                                    mbrSystemId: 0x0C, gptType: Gpt.BasicDataPartitionType))
        {
            Console.WriteLine("Create failed");
            return;
        }
        else
        {
            Console.WriteLine("Partition created successfully.");
        }
    }
    public static void LNXLUA(string Path, string arguments)
    {
       
            try
            {
                LuaInterpreter lua = new()
                {
                    WorkingDirectory = $"{Directory.GetCurrentDirectory()}"  
                };
                lua.DoFile($"{Path}",arguments);
               
            }
            catch (LuaException e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine(e.LuaStackTrace);
            }
            return;
        
    }
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
                    if(Directory.Exists("/mnt/LNXsys") && !panic_inpanic)
                    {
                    File.AppendAllText("/mnt/LNXsys/Panic.log", $"BSOD at {DateTime.Now}, Name: {text}, ERROR code:{id}\n");
                    } else
                    {
                        if(!panic_inpanic)
                        {
                            Console.WriteLine("Panic on panic");
                        }
                        else
                        {
                            Directory.CreateDirectory("/mnt/LNXsys");
                            Console.WriteLine("Failed to make the panic.log file");
                        }

                    }
                    panic_inpanic = true;
                    txtMgr.error("ERROR CODE ID:");
                    txtMgr.customNL(ConsoleColor.DarkRed, $"{id}\n\n");
                    txtMgr.error($"ERROR CODE NAME:");
                    
                    txtMgr.customNL(ConsoleColor.DarkRed, $"{text}\n\n");
                    txtMgr.aprove($"IT IS SAFE TO REBOOT NOW OR WAIT 5 SECONDS[{text},{id}]");
                    Thread.Sleep(5000);
                    Power.Reboot(); 
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