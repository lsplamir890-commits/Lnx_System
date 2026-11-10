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
using System.Reflection;
using Cosmos.Kernel.Plugs.System;
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
    public static void Shutdown()
    {
        try
        {
            Power.Shutdown();
        }
        catch
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            txtMgr.aprove("It is safe to shutdown your computer");
            halt();            
        }
        if(Power.IsSupported)
        {

        }
    }
    public static bool Chckpass()
    {
        while(true)
        {
            Console.Write("Password: ");
            string? checkpsa = Console.ReadLine().ToLower();
            if(UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "password") == checkpsa)
            {
                txtMgr.aprove("Correct password.");
                return true;
            }
            else
            {
                txtMgr.error("WRONG PASSWORD");
            }
        }

    } 
    public static void listfolder()
    {
       if (VfsManager.TryStatFs("/mnt", out VfsStatFs stats))
        {
            ulong freeBytes = stats.BlockSize * stats.BlockSize;
            ulong totalBytes = stats.Blocks * stats.BlockSize;
                Console.WriteLine($"{freeBytes} of {totalBytes} On partiton\nCurrent directory: {Directory.GetCurrentDirectory()}");
        }
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
        string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
            
        foreach (string filez in files)
        {
            txtMgr.custom(ConsoleColor.Blue,"[File] ");
            Console.WriteLine($"{filez.Split(Directory.GetCurrentDirectory() + "/")[1]}");
        }
        Console.WriteLine();
        foreach (string dir in dirs)
        {
            txtMgr.custom(ConsoleColor.Yellow,"[DIR] ");
            Console.WriteLine($"{dir.Split(Directory.GetCurrentDirectory() + "/")[1]}");
        }
    }
    public static void Format(int id,string MountPath, IBlockDevice? disk)
    {
        //int ind = 1;
        IReadOnlyList<Partition> partitions = StorageManager.GetPartitions(disk);
        bool isOnLoop = true;
        try
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            // while(true)
            // {

            //     int inx = 1;
            //     if(inx == 1)
            //     {
            //         Console.Clear();
            //         txtMgr.normal($"====LNXOS PARTITION MANAGER====\nThis will help select the partition to format your drive");
            //         if(partitions[inx] == null)
            //         {
            //             txtMgr.error($"Partition {inx} is null and is required for formatting.");
            //             halt();
            //         }
            //         txtMgr.normal($"[>] Partition {partitions[0].Name}");
            //         if(partitions[1] != null)txtMgr.normal($"[ ] Partition {partitions[1].Name}");
            //         if(partitions[2] != null)txtMgr.normal($"[>] Partition {partitions[2].Name}");
            //         ConsoleKeyInfo key = Console.ReadKey(true);
            //         if(key.Key == ConsoleKey.Enter)
            //         {
            //             NoPromptFormat("LNXOS", inx, MountPath, MountIt:true);
            //             halt();
            //         }
            //         if(key.Key == ConsoleKey.DownArrow)
            //         {
            //             inx++;
            //         }
                    
            //     }
            //     else if(inx == 2)
            //     {
            //         Console.Clear();
            //         txtMgr.normal($"[ ] Partition {partitions[0].Name}");

            //         if(partitions[1] != null)txtMgr.normal($"[ ] Partition {partitions[1].Name}");
            //         if(partitions[2] != null)txtMgr.normal($"[>] Partition {partitions[2].Name}");

            //         ConsoleKeyInfo key = Console.ReadKey(true);
            //         if(key.Key == ConsoleKey.Enter && partitions[1] != null)
            //         {
            //             NoPromptFormat("LNXOS", inx, MountPath, MountIt:true);
            //             halt();
            //         }
            //         if(key.Key == ConsoleKey.UpArrow)
            //         {
            //             inx--;
            //         }
            //         if(key.Key == ConsoleKey.DownArrow)
            //         {
            //             inx++;
            //         }
            //     }
            //     else
            //     {
            //         txtMgr.normal($"[ ] Partition {partitions[inx].Name}");
            //         if(partitions[1] != null)txtMgr.normal($"[ ] Partition {partitions[1].Name}");
            //         if(partitions[2] != null)txtMgr.normal($"[>] Partition {partitions[2].Name}");
            //         ConsoleKeyInfo key = Console.ReadKey(true);
            //         if(key.Key == ConsoleKey.Enter &&  partitions[2] != null)
            //         {
            //             NoPromptFormat("LNXOS", inx, MountPath, MountIt:true);
            //             halt();
            //         }
            //         if(key.Key == ConsoleKey.UpArrow)
            //         {
            //             inx--;
            //         }
            //     }
                
                
            // }

            
            txtMgr.normal("Starting formatting/reformmating...");
            txtMgr.normal("Formatting /mnt/ partition...");
            Console.Write("PARTITION NAME: ");
            string? partname = Console.ReadLine();
            VfsManager.TryUnmount(MountPath);
            FatFormatOptions options = new()
            {
                Type = FatType.Fat32,
                VolumeLabel = $"{partname}     ",
            };

            if (StorageManager.Partitions.Count == 0
                    || !VfsManager.TryFormat("fat", StorageManager.Partitions[id], options))
            {
                txtMgr.error("Format failed");
            }
            VfsManager.TryMount("fat", StorageManager.Partitions[id], MountFlags.None, MountPath, out VfsMount? mount);

            txtMgr.aprove("Formatting completed. It is safe to reboot your pc. DO NOT REMOVE THE CD OR USB");
            UtilityLNX.halt();
        }
        catch
        {
            UtilityLNX.Panic("INSTALLER_FORMAT_FAIL", 0x0e001);
        }
    }
    public static void NoPromptFormat(string name, int partition,string mountpath, bool MountIt = false)
    {
        try
        {
            VfsManager.TryUnmount(mountpath);
            FatFormatOptions options = new()
            {
                Type = FatType.Fat32,
                VolumeLabel = $"{name}     ",
            };

            if (!VfsManager.TryFormat("fat", StorageManager.Partitions[partition], options))
            {
                txtMgr.error("Format failed");
            }
            if(MountIt == true) VfsManager.TryMount("fat", StorageManager.Partitions[1], MountFlags.None, mountpath, out VfsMount? mount);
            return;
        }
        catch
        {
            UtilityLNX.Panic("INSTALLER_FORMAT_FAIL", 0x0e001);
        }
    }

    public static void createpart()
    {

        
         IBlockDevice? disk = StorageManager.PrimaryDevice;
        Mbr.Create(disk);
        PartitionManager.PartitionLocation location = new(2048,2097152 );
        
        txtMgr.normal("Sectors 2048 to 2097152  are about to be overwritten. Are you sure you wanna continue? (y,n)");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if(key.Key != ConsoleKey.Y)
        {
            txtMgr.error("Your computer will now halt..");
            halt();
        }
        if(PartitionManager.Delete(disk, location))
        {
            txtMgr.normal("an existing Partition was found!");
            txtMgr.info("Deleted");
        }
        // Gpt.Parse(disk);    

        //Make esp partition
        if (!PartitionManager.Create(disk, startSector: 2048, sectorCount: 1026048 ,
                                    mbrSystemId: 0xEF, gptType:Gpt.BasicDataPartitionType))
        {
            txtMgr.error("failed while creating the ESP partition starting from 2048 sector and is 1026048 sectors long ");
            halt();
        }
        
        else
        {
            txtMgr.aprove("esp partition created succesfully Now formatting esp part.."); 
            StorageManager.RescanPartitions(disk);
            UtilityLNX.NoPromptFormat("LNXOSEFI",0, "/efi");
            InstallService.ESPINSTALL();
        }
        Thread.Sleep(1000);
        if(!PartitionManager.Create(disk,startSector: 1028096, sectorCount: 1069056, 0x0c,gptType:Gpt.BasicDataPartitionType))
        {
            txtMgr.error("failed while creating the main partition...");
            halt();
        }
        else
        {
            StorageManager.RescanPartitions(disk);
            UtilityLNX.Format(1,"/mnt",disk);
        }
                //Gpt.AddPartition(disk,2048,131072, Guid.NewGuid());
       


        
    }

    public static void Panic(string text, long id)
    {
       
        try
        {
                    KernelConsole.Default.Font = PCScreenFont.DefaultFont;
                    Console.Beep();
                    Console.CursorVisible = false;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    txtMgr.normal($"{text} at {id}");
                    txtMgr.normal($"Fatal!");
                    txtMgr.normal("a fatal problem has occured on the kernel\nAnd it has been stopped for your computer safety\n");
                    txtMgr.normal("Writing a log\n\n");
                    txtMgr.normal("0%");
                    File.AppendAllText("/mnt/LNXsys/Panic.log", $"BSOD at {DateTime.Now}, Name: {text}, ERROR code:{id}\n");
                    txtMgr.normal("100%");
                    
                    Thread.Sleep(5000);
                    Power.Reboot(); 
        } 
        catch
        {
            
            SafePanic("PANIC_IN_PANIC", "0x0");
        }
       

    }
    public static void SafePanic(string text, string id)
    {
        KernelConsole.Default.Font = PCScreenFont.DefaultFont;
        Console.Beep();
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        txtMgr.normal("YOUR PC HAS CRASHED \n");
        Console.BackgroundColor = ConsoleColor.Blue;
        txtMgr.normal("A problem has occured\n\n");
        txtMgr.normal("since this is a safe panic. No log file has been made");
                
        txtMgr.error("ERROR CODE ID:");
        txtMgr.customNL(ConsoleColor.DarkRed, $"{id}\n\n");
        txtMgr.error($"ERROR CODE NAME:");
                    
        txtMgr.customNL(ConsoleColor.DarkRed, $"{text}\n\n");
        txtMgr.aprove($"IT IS SAFE TO REBOOT[{text},{id}]");
        halt();
        
       

    }
    public static void halt()
    {
        while(true) Power.Halt();
    }
    public static bool FindIniKey(string path, string key)
    {
        string[] file = File.ReadAllLines(path);
        foreach(string line in file)
        {
            if(line.StartsWith(key))
            {
                return true;
            }
        }   
        return false;
    }
    public static string ReadIni(string Path, string target)
    {
        string[] file = File.ReadAllLines(Path); 
        foreach(string line in file)
        {
            if(line.StartsWith(target))
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