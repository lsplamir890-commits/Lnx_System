//Dev of lnxos 1
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
using Cosmos.Executable.Lua;
using Mono.Cecil;

namespace lnkrnl;

/// <summary>
/// Main kernel class - inherits from Cosmos.Kernel.System.Kernel.
/// </summary>
public class Kernel : Sys.Kernel
{

    static bool man = false;   
    static string DefaultPartName = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "FsName");
    

    protected override void BeforeRun()
    {
        

        
        
        IBlockDevice? disk = StorageManager.PrimaryDevice;
        FatFilesystemType fat = new();

        if (!VfsManager.RegisterFilesystem("fat", fat))
        {
            Console.WriteLine("The name \"fat\" is already registered.");
            return;
        }

        StorageManager.RescanPartitions(disk);
        if (StorageManager.Partitions.Count == 0)
        {
            
            Console.WriteLine("No partitions found.");
            try
            {
                txtMgr.normal("Starting partitioning...");
                UtilityLNX.createpart();
                Power.Reboot();
            }
            catch
            {
                Console.Clear();
                txtMgr.warn("SYSTEM HALTED. could not partition your disk. Please restart and DO NOT REMOVE THE CD OR USB") ;
                 Power.Shutdown();
            }
            return;
        }
        if (VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.None, "/mnt", out VfsManager.VfsMount? mount))
        {
            Console.WriteLine("Mounted " + mount.Name + " at " + mount.MountPoint);
        }
        if(!File.Exists("/mnt/LNXsys/System64/instLuviz.lze") && !File.Exists("/mnt/lnxOSpage.lua") && !File.Exists("/mnt/Users/lnchck"))
        {
            InstallService.part1();

        }
        if(!File.Exists("/mnt/LNXsys/System64/LunDos.ini"))
        {
            try
            {
                if(Directory.Exists("/mnt/LNXsys/System64"))
                {
                    File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", "username=LN_KERNEL_RECOVERY");
                }
                else
                {
                    Directory.CreateDirectory("/mnt/LNXsys/System64");
                    File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", "username=LN_KERNEL_RECOVERY");
                }
                
            }
            catch
            {
                UtilityLNX.Panic("Could not repair lundos.ini", "0x0000ef");
            }
            UtilityLNX.Panic("LunDos Is dead", "0xEa0FDf");
        }
        try
        {
             Directory.SetCurrentDirectory($"/mnt/Users/{username}");
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
        if(guionboot == "true")
        {
            txtMgr.aprove("Gui on boot is true");
            Desktop desktop = new Desktop();
            desktop.launch();
        }
    }
    static int line = 0;
    static string guionboot = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini","GuiOnBoot");
    static public string username = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "username");
    protected override void Run()
    {

        if(man == true)
        {
            username = "Native_LN_COSMOS";
        }
        line ++;
        
        try
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{username}");
           
            Console.Write($"@:~{Directory.GetCurrentDirectory()} $ ");
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
        
        else if(input.StartsWith("lua "))
        {
            string file = input.Split(" ")[1].Trim();
            string arguments = input.Substring($"lua {file}".Length).Trim();
            UtilityLNX.LNXLUA(file, arguments);
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
        else if(input.StartsWith("editor"))
{
            string filename = input.Substring(7);
            editor.launch($"{Directory.GetCurrentDirectory()}/{filename}");
            return;
        }
        switch (input.ToLower())
        {
            case "reboot":
                txtMgr.warn("rebooting...");
                Power.Reboot();
            break;
            case "collect":
                txtMgr.info("using gc.collect()...");
                try
                {
                    UtilityLNX.Panic("TESTING.", "0x000000");
                }
                catch
                {
                    txtMgr.error("Could not collect the garbage");
                }
            break;
            case "gui":
                txtMgr.error("Gui is on testing. Are you sure you want to start it?");
                Desktop desktop = new Desktop();
                desktop.launch();
            break;
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
            case "ext2":
                Newshell.Launch();
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
                 if (VfsManager.TryStatFs("/mnt", out VfsStatFs stats))
                {
                    ulong freeBytes = stats.Bavail * stats.BlockSize;
                    ulong totalBytes = stats.Blocks * stats.BlockSize;
                    Console.WriteLine($"{freeBytes} of {totalBytes} On partiton\nCurrent directory: {Directory.GetCurrentDirectory()}");
                }
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

            if(File.Exists("/mnt/LNXsys/System64/LunDos.ini"))
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
             if(File.Exists("/mnt/LNXsys/System64/instLuviz.lze"))
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
            if(Directory.Exists("/mnt/LNXsys"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LNXsys exists!!");
                Console.ForegroundColor = ConsoleColor.White;
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("LNXsys directory isnt healthy");
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
                if(File.Exists("/mnt/LNXsys/System64/instLuviz.lze"))
                {
                    if(Directory.Exists("/mnt/LNXsys/System64"))
                    {
                        if(File.Exists("/mnt/LNXsys/System64/LunDos.ini"))
                        {
                            File.Delete("/mnt/LNXsys/System64/LunDos.ini");
                        }
                        if(File.Exists("/mnt/LNXsys/System64/instLuviz.lze"))
                        {
                            File.Delete("/mnt/LNXsys/System64/instLuviz.lze");
                        }
                        if(File.Exists("/mnt/users/lnchck"))
                        {
                            File.Delete("/mnt/Users/lnchck");
                        }
                        if(File.Exists("/mnt/lnxOSpage.lua"))
                        {
                            File.Delete("/mnt/lnxOSpage.lua");
                        }
                        Directory.Delete("/mnt/LNXsys/System64", true);
                    }

                    
                   
                    

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
                Console.WriteLine("  reboot     - Reboots the computer");
                Console.WriteLine("  gui     - launches gui mode");
                Console.WriteLine("  help-2     - shows more commands");
                break;
            case "help-2":
                Console.WriteLine("  lua <path> <Extra arguments>     - Lua interpreter");
                Console.WriteLine("  editor <path>     - File editor");
                Console.WriteLine("  luaenv     - Shows the lua envoriment");
            break;
            case "luaenv":
                UtilityLNX.luaenvoriment();
            break;

            case "ver":
                UtilityLNX.LNXLUA("/mnt/LNXsys/bin/ver.lua", "");
            break;
            
            case "abt":
                UtilityLNX.LNXLUA("/mnt/LNXsys/bin/about.lua", "");
             //Console.WriteLine("Made on: Gen 3 v3.0.88 COSMOS");
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
