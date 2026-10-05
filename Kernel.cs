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
namespace lnkrnl;

/// <summary>
/// Main kernel class - inherits from Cosmos.Kernel.System.Kernel.
/// </summary>
public class Kernel : Sys.Kernel
{
    
    static string DefaultPartName = ReadIni("/mnt/LunDos.ini", "FsName");
    Stream? resource = assembly.GetExecutingAssembly().GetManifestResourceStream("promaxkernel.EmbeddedFile.txt");
    static string ReadIni(string Path, string target)
    {
        string[] mainsetupfile = File.ReadAllLines("/mnt/LunDos.ini"); 
        foreach(string line in mainsetupfile)
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
    static void createfile(string name, string conent)
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
    protected override void BeforeRun()
    {
        IBlockDevice? disk = StorageManager.PrimaryDevice;
        FatFilesystemType fat = new();

        if (!VfsManager.RegisterFilesystem("fat", fat))
        {
            Console.WriteLine("The name \"fat\" is already registered.");
            return;
        }
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
        StorageManager.RescanPartitions(disk);
        if (StorageManager.Partitions.Count == 0)
        {
            Console.WriteLine("No partitions found.");
            return;
        }
        if (VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.None, "/mnt", out VfsManager.VfsMount? mount))
        {
            Console.WriteLine("Mounted " + mount.Name + " at " + mount.MountPoint);
        }
        if(!File.Exists("/mnt/instLuviz.lze"))
        {

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            // using var reader = new StreamReader(resource);
            // string content = reader.ReadToEnd();
            // Console.WriteLine("Embedded file content: " + content);

            Console.WriteLine("SETUP LOADED IN: ");
            Console.WriteLine("You must have a nvme drive or achi drive for the setup to work");
            Console.WriteLine("Press F2 to start setup");
            Console.WriteLine("Press F3 to reformat or format the drive(format the disk if you never did)"); 
            ConsoleKeyInfo  key = Console.ReadKey(true);
            switch(key.Key)
            {
                case ConsoleKey.F2:
                    try
                    {
                        Console.WriteLine("Starting setup...");
                        File.Create("/mnt/instLuviz.lze");
                        File.AppendAllText("/mnt/instLuviz.lze", "[systeminstall] = true");
                        Directory.CreateDirectory("/mnt/SysSetts/files");
                        File.AppendAllText("/mnt/LunDos.ini","[setup]\n");

                        //name
                        Console.Write("Type your name: ");
                        string user = Console.ReadLine().Trim();
                        File.AppendAllText("/mnt/LunDos.ini",$"username={user}\n");
            
                        //filesystem
                        Console.Write("Default partition name:");
                        string Fs = Console.ReadLine().Trim();
                        File.AppendAllText("/mnt/LunDos.ini",$"FsName={Fs}\n");

                        Console.WriteLine("Setup completed. It is safe to reboot your pc. DO NOT REMOVE THE CD OR USB");
                        Power.Shutdown();
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("SYSTEM HALTED. Couldnt Setup your pc. Please restart and DO NOT REMOVE THE CD OR USB\nextra info: This may have happend because you didnt format your disk or\nit isnt supported") ;
                        Power.Shutdown();
                    }
                    break;
                case ConsoleKey.F3:
                    try
                    {
                        Console.WriteLine("Starting formatting/reformmating...");
                        Console.WriteLine("Formatting /mnt/ partition...");
                        VfsManager.TryUnmount("/mnt");
                        FatFormatOptions options = new()
                        {
                            Type = FatType.Fat32,
                            VolumeLabel = $"COSMOS     ",
                        };

                        if (StorageManager.Partitions.Count == 0
                            || !VfsManager.TryFormat("fat", StorageManager.Partitions[0], options))
                        {
                            Console.WriteLine("Format failed");
                        }   
                        VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.None, "/mnt", out VfsManager.VfsMount? mountO);
                    
                        Console.WriteLine("Formatting completed. It is safe to reboot your pc. DO NOT REMOVE THE CD OR USB");
                        Power.Shutdown();
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("SYSTEM HALTED. could not format your disk. Please restart and DO NOT REMOVE THE CD OR USB") ;
                        Power.Shutdown();
                    }

                    
                    break;
                    default:
                    Console.WriteLine("Couldnt install since you Didnt press f2 or f3 It is safe to reboot your pc. DO NOT REMOVE THE CD OR USB");
                    break;
                
            }
        }
        try
        {
             Directory.SetCurrentDirectory("/mnt");
        }
        catch
        {
            Console.WriteLine("Couldnt set the current directory to Primary partition");
        }
        Console.WriteLine("Please wait..");
        Thread.Sleep(1500);
        Console.Clear();
        if (VfsManager.TryStatFs("/mnt", out VfsStatFs stats))
        {
            ulong freeBytes = stats.Bavail * stats.BlockSize;
            ulong totalBytes = stats.Blocks * stats.BlockSize;
            Console.WriteLine($"{freeBytes} bytes of hdd left [OK]");
        }
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("KERNEL booted successfully!");
        Console.WriteLine("tip: If you want to see all commands type 'help' and press enter.\nAlso This is a beta under Development ");

    }
    static int line = 0;
    static string username = ReadIni("/mnt/LunDos.ini", "username");
    protected override void Run()
    {
        line ++;
        
        try
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{username}");
           
            Console.Write($"@~ $ {Directory.GetCurrentDirectory()} > ");
        }
        catch
        {
            Console.Write("root@~ $ UNKNOWNDRIVE > ");
        }
        Console.ForegroundColor = ConsoleColor.White;
        var input = Console.ReadLine().ToLower();

        if (string.IsNullOrEmpty(input))
            return;
        if(input.StartsWith("mkdir -p "))
        {
            string directory = input.Substring("mkdir -p ".Length).Trim();
            try
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Directory '{directory}' created successfully.");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to create directory '{directory}'");
            }
            return;
        }
        else if(input.StartsWith("cd "))
        {
            string directory = input.Split(" ")[1].Trim();
            try
            {
                
                Directory.SetCurrentDirectory($"{Directory.GetCurrentDirectory()}/{directory}");
                
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{directory} is missing or its corrupt and damaged");
            }
            return;
        } 
        else if(input.StartsWith("del "))
        {
            string sector = input.Split(" ")[1].Trim();
            try
            {
                if(File.Exists(sector))
                {
                     File.Delete($"{sector}");
                }
                else
                {
                    Directory.Delete($"{sector}", true);
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This isnt a valid file/folder or it  doesnt exist");
            }
            return;
        }     
        else if(input.StartsWith("echo > "))
        {
            string sector = input.Split(" ")[2].Trim();
            try
            { 
                Console.WriteLine(File.ReadAllText($"{Directory.GetCurrentDirectory()}/{sector}"));

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This isnt a valid file or it  doesnt exist");
            }
            return;
        }   
        else if(input.StartsWith("echo < "))
        {
            string filename = input.Split(" ")[2].Trim();
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type the text :");
                string  text = Console.ReadLine();
                File.WriteAllText($"{Directory.GetCurrentDirectory()}/{filename}", text);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Couldnt make a new file");
            }
            return;
        }
        else if(input.StartsWith("echo "))
        {
            string filename = input.Substring(5);
            Console.WriteLine(filename);
            return;
        }
        switch (input.ToLower())
        {
            case "date":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{DateTime.Now}");
            break;
            case "format":
                VfsManager.TryUnmount("/mnt");
                FatFormatOptions options = new()
                {
                    Type = FatType.Fat32,
                    VolumeLabel = $"{DefaultPartName}     ",
                };

                if (StorageManager.Partitions.Count == 0
                    || !VfsManager.TryFormat("fat", StorageManager.Partitions[0], options))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Format failed");
                }   
                VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.None, "/mnt", out VfsManager.VfsMount? mount);
                
            break;
          
            case "maketestfile":
            try
            {
                using FileStream stream = File.Create("/mnt/testing.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            break;
            case "ls":
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|Files| \n");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
            Console.Write("|Directories| \n");
            foreach (string dir in dirs)
            {
                Console.WriteLine( dir);
            }

            break;
            case "lnkrnl -check":
            int errors = 0;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Checking LnxOS ");

            if(File.Exists("/mnt/LunDos.ini"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LunDos.ini is healthy");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("LunDos.ini Doesnt exist or its corrupted");
                Console.ForegroundColor = ConsoleColor.White;
                errors =+ 1;
            }
             if(File.Exists("/mnt/instLuviz.lze"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("instLuviz.lze is healthy");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("instLuviz.lze Doesnt exist or its corrupted");
                Console.ForegroundColor = ConsoleColor.White;
                errors =+ 1;
            }            
            if(Directory.Exists("/mnt/SysSetts"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SysSetts is healthy!");
                Console.ForegroundColor = ConsoleColor.White;
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SysSetts directory isnt healthy");
                Console.ForegroundColor = ConsoleColor.White;
                errors =+ 1;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"There are: {errors}. \nIf theres any errors Please fix your installation");
            break;
            case "re-setup":
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                
                Console.WriteLine("Please press any key to restart");

                Console.ReadKey();
                if(File.Exists("/mnt/instLuviz.lze"))
                {
                    File.Delete("/mnt/instLuviz.lze");
                }
                Power.Reboot();
            break;
            case "help":
                Console.ForegroundColor = ConsoleColor.Green;
            
                Console.WriteLine("Available commands:");
                Console.WriteLine("  echo > <filename> - Read a file and it displays text");
                Console.WriteLine("  echo < <filename> - Writes a file to the primary partition");
                Console.WriteLine("  format   - Format the primary partition with FAT32");
                Console.WriteLine("  maketestfile - Create a test file in the root directory");
                Console.WriteLine("  ls      - List files in the current directory");
                Console.WriteLine("  mkdir -p <directory> - Create a directory ");
                Console.WriteLine("  help     - Show this help message");
                Console.WriteLine("  del <file/directory>    - Deletes a file or directory");
                Console.WriteLine("  cd <directory>     - changes directory");
                Console.WriteLine("  clear    - Clear the screen");
                Console.WriteLine("  halt     - Halt the system");
                Console.WriteLine("  date     - shows the date"); 
                Console.WriteLine("  ver     - Shows the os version");
                Console.WriteLine("  abt     - Shows some data");
                Console.WriteLine("  echo     - echos the text you inputed");
                Console.WriteLine("  re-setup     - will ask you to reboot your computer and you will be taken to the setup");
                Console.WriteLine("  lnkrnl -check     - checks LnxOS for any problems");
                break;
            case "ver":
                Console.WriteLine("Dev Test Thanks for trying this os");
            break;
            
            case "abt":
             Console.WriteLine("Made by jsplashh");
             Console.WriteLine("Simple but a little creative os");
             Console.WriteLine("Kernel: lnkrnl\nKernel Achitecture: ln");
             Console.WriteLine("Thanks for reading this and trying this");
             Console.WriteLine("Made on: Gen 3 v3.0.88 COSMOS");
            break;
            case "clear":
                Console.Clear();
                break;

            case "halt":
                Console.WriteLine("Halting system...");
                Stop();
                break;
            
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Shell Failed at line:{line}: \"{input}\" is not a command");
                break;
            
        }
        GC.Collect();
    }
}
