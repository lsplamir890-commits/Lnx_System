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
public class InstallService
{
    public static void part1()
    {
        while(true)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

                // using var reader = new StreamReader(resource);
                // string content = reader.ReadToEnd();
                // Console.WriteLine("Embedded file content: " + content);
                Console.BackgroundColor = ConsoleColor.Gray;    
            txtMgr.normal("=====LNX OS INSTALLER=====");
            Console.BackgroundColor = ConsoleColor.Blue;  
            txtMgr.normal("You must have a nvme drive or achi drive for the setup to work");
                
            txtMgr.normal("\n\nYou must press F2 or F3\nThis is a beta so expect bugs\nSome times it will break so much it may format your drive\nMade by jsplashh 2026 (C)\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.BackgroundColor = ConsoleColor.Gray;  
            
            Console.Write("\nF2 to start setup | Press F3 reformat/format");
            Console.BackgroundColor = ConsoleColor.Blue;  
            ConsoleKeyInfo  key = Console.ReadKey(true);
            switch(key.Key)
            {
                case ConsoleKey.F2:
                    try
                    {
                        Console.Clear();
                        txtMgr.normal("Starting setup...");
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
                        part2();    
                        txtMgr.aprove("Setup completed. It is safe to reboot your pc. DO NOT REMOVE THE CD OR USB");
                        Power.Shutdown();
                    }
                    catch
                    {
                        Console.Clear();
                        txtMgr.error("SYSTEM HALTED. Couldnt Setup your pc. Please restart and DO NOT REMOVE THE CD OR USB\nextra info: This may have happend because you didnt format your disk or\nit isnt supported") ;
                        Power.Shutdown();
                    }
                    break;
                case ConsoleKey.F3:
                    try
                    {
                        txtMgr.normal("Starting formatting/reformmating...");
                        txtMgr.normal("Formatting /mnt/ partition...");
                        VfsManager.TryUnmount("/mnt");
                        FatFormatOptions options = new()
                        {
                            Type = FatType.Fat32,
                            VolumeLabel = $"COSMOS     ",
                        };

                        if (StorageManager.Partitions.Count == 0
                            || !VfsManager.TryFormat("fat", StorageManager.Partitions[0], options))
                        {
                                txtMgr.error("Format failed");
                        }   
                        VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.None, "/mnt", out VfsManager.VfsMount? mountO);
                        
                        txtMgr.aprove("Formatting completed. It is safe to reboot your pc. DO NOT REMOVE THE CD OR USB");
                        Power.Shutdown();
                    }
                    catch
                    {
                        Console.Clear();
                        txtMgr.warn("SYSTEM HALTED. could not format your disk. Please restart and DO NOT REMOVE THE CD OR USB") ; //yes i used warn instead of error
                        Power.Shutdown();
                    }

                        
                    break;
                    case ConsoleKey.F4:
                    File.AppendAllText("/mnt/LunDos.ini",$"username=Native_LN_COSMOS\n");
                    txtMgr.normal("Starting envoriment");
                    Console.BackgroundColor = ConsoleColor.Black;
                        
                    return;            
            }

                    

                
            }
    }
    public static void part2()
    {
        bool selection1 = true;

        Console.Clear();
        while(true)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal("====MORE INFO BEFORE REBOOT====");
            Console.BackgroundColor = ConsoleColor.Blue;
            txtMgr.normal("To navigate through the menu Just press 1 or 2\n");
            txtMgr.normal("Do you want Gui on boot?");
            if(!selection1 == true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("Yes I want gui on boot");
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("No i dont want gui on boot");
                Console.BackgroundColor = ConsoleColor.Blue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.D1:
                        selection1 = true;
                    break;
                    case ConsoleKey.Enter:
                        File.AppendAllText("/mnt/LunDos.ini", "GuiOnBoot=false");
                        part3();
                    break;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("Yes I want gui on boot");
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("No i dont want gui on boot");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.D2:
                        selection1 = false;
                    break;
                    case ConsoleKey.Enter:
                        File.AppendAllText("/mnt/LunDos.ini", "GuiOnBoot=true");
                        part3();
                    break;
                }
            }

        }
    }
    public static void part3()
    {
        bool selection1 = true;
        while(true)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal("====MORE INFO BEFORE REBOOT====");
            Console.BackgroundColor = ConsoleColor.Blue;
            txtMgr.normal("To navigate through the menu Just press 1 or 2\n");
            txtMgr.normal("Do you want to reboot? (this is just filler)");
            if(!selection1 == true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("Yes, please reboot");
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("No, just halt the computer");
                Console.BackgroundColor = ConsoleColor.Blue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.D1:
                        selection1 = true;
                    break;
                    case ConsoleKey.Enter:
                        while(true)
                        Power.Halt();
                    
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("Yes, please reboot");
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("No, just halt the computer");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.D2:
                        selection1 = false;
                    break;
                    case ConsoleKey.Enter:
                        Power.Reboot();
                    break;
                }
            }        
        }

    }
}