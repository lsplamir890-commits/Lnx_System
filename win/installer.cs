//Dev of lnxos 1
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
namespace lnkrnl;

public class InstallService
{
    public static void ESPINSTALL()
    {
        VfsManager.TryUnmount("/efi");
        VfsManager.TryMount("fat",StorageManager.Partitions[0],MountFlags.None,"/efi",out VfsMount? mountefi);
         Directory.CreateDirectory("/efi/EFI/BOOT");
         File.AppendAllBytes("/efi/EFI/BOOT/BOOTX64.EFI", EmbeddedResource.ReadBytes("lnkrnl.resources.BOOTX64.EFI"));
    }
    public static void LimineInstaller()
    {
        //RawInstall(EmbeddedResource.ReadBytes("lnkrnl.resources.boot.bin"),0);
        Directory.CreateDirectory("/mnt/boot/limine");
       
        Directory.CreateDirectory("/mnt/boot/host.wui");
        Directory.CreateDirectory("/mnt/lnxsys/system64/host"); //DIRECTORY FOR FUTURE USE

        File.AppendAllText("/mnt/boot/limine/limine.conf", EmbeddedResource.ReadString("lnkrnl.resources.limine.conf"));
        File.AppendAllBytes("/mnt/boot/host.wui/liminepaper.png", EmbeddedResource.ReadBytes("lnkrnl.resources.coolwallpaper.png")); // we install it once again cuz limine 
        //doesnt like paths taht arent inside boot or on the main mount
        File.AppendAllBytes("/mnt/boot/host.wui/kernel.host.elf", EmbeddedResource.ReadBytes("lnkrnl.resources.lnkrnl.elf"));
        ESPINSTALL();
        //File.AppendAllBytes("/mnt/boot/limine/limine-bios.sys", EmbeddedResource.ReadBytes("lnkrnl.resources.limine-bios.sys"));
    }
    public static void part1()
    {

        taskMgr.init("PREINSTALLENV");
        while (true)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();

            // using var reader = new StreamReader(resource);
            // string content = reader.ReadToEnd();
            // Console.WriteLine("Embedded file content: " + content);
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal("=====LNX OS 1 INSTALLER=====");
            Console.BackgroundColor = ConsoleColor.Blue;
            txtMgr.normal("LnxOS Installer will install lnxos on your computer\nit is reccomended that your computer is uefi x64.");

            txtMgr.normal($"\n\nYou must press F2 or F3\nLnxOS is open source:\nhttps://github.com/lsplamir890-commits/Lnx_System/\n\nMade by jsplashh 2026 (C)\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.BackgroundColor = ConsoleColor.Gray;

            Console.Write("\nF2 to start setup | Press F3 reformat/format");
            Console.BackgroundColor = ConsoleColor.Blue;
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.F2:
                    try
                    {

                        Console.Clear();
                        txtMgr.normal("                        [---------------]");
                        txtMgr.normal("                     making directory strucutre");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Directory.CreateDirectory("/mnt/LNXsys/System64");
                        Directory.CreateDirectory("/mnt/LNXsys/bin");
                        Directory.CreateDirectory("/mnt/LNXsys/Themes");
                        Directory.CreateDirectory("/mnt/Users");
                        Directory.CreateDirectory("/mnt/LNXsys/Fonts");
                        Directory.CreateDirectory("/mnt/ProgramsX64");
                        txtMgr.normal("                        [####-----------]");
                        txtMgr.normal("                    installing fonts, wallpaper and cursor");
                        Thread.Sleep(1000);
                        Console.Clear();
                        File.AppendAllBytes("/mnt/LNXsys/System64/cursor.png", EmbeddedResource.ReadBytes("lnkrnl.resources.Cursor.png")); //1
                        File.AppendAllBytes("/mnt/LNXsys/System64/BootIcon.png", EmbeddedResource.ReadBytes("lnkrnl.resources.icon.png")); //2
                        File.AppendAllBytes("/mnt/LNXsys/Themes/nebula.png", EmbeddedResource.ReadBytes("lnkrnl.resources.coolwallpaper.png")); //3
                        File.AppendAllBytes("/mnt/LNXsys/Fonts/lol.ttf", EmbeddedResource.ReadBytes("lnkrnl.resources.comic.ttf")); // 4
                        txtMgr.normal("                        [#########-------]");
                        txtMgr.normal("                       installing extra png's");
                        Thread.Sleep(1000);
                        Console.Clear();
                        File.AppendAllBytes("/mnt/LNXsys/System64/test.wav", EmbeddedResource.ReadBytes("lnkrnl.resources.coolmusic.wav")); // 5
                        File.AppendAllBytes("/mnt/LNXsys/System64/error.png", EmbeddedResource.ReadBytes("lnkrnl.resources.error.png")); // 6
                        File.AppendAllBytes("/mnt/LNXsys/System64/warn.png", EmbeddedResource.ReadBytes("lnkrnl.resources.warn.png")); // 7
                        File.AppendAllBytes("/mnt/LNXsys/System64/info.png", EmbeddedResource.ReadBytes("lnkrnl.resources.info.png")); // 8
                        Console.Clear();
                        txtMgr.normal("                         [#############--]");
                        txtMgr.normal("         installing bootloader, kernel and final resources");
                        File.AppendAllText("/mnt/lnxOSpage.lua", $"print(\"check install\")\n");
                        File.AppendAllText("/mnt/LNXsys/bin/about.lua", $"print(\"Made by jsplashh\")\nprint(\"Simple os but is good\")\nprint(\"Made by jsplashh \\nArchitecture and kernel: LN \")\n");
                        File.AppendAllText("/mnt/LNXsys/bin/ver.lua", $"print(\"On dev\")\n");
                        File.Create("/mnt/LNXsys/System64/instLuviz.lze");
                        File.Create("/mnt/Users/lnchck");
                        File.AppendAllText("/mnt/LNXsys/System64/instLuviz.lze", "[systeminstall] = true");
                        File.AppendAllBytes("/mnt/LNXsys/Fonts/mainfont.ttf", EmbeddedResource.ReadBytes("lnkrnl.resources.regular.ttf"));

                        File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", "[setup]\n");
                        LimineInstaller();
                        txtMgr.normal("                         [###############]");
                        txtMgr.normal("                         installer is done  ");
                        //name

                        //part2();
                        Console.Clear();
                        txtMgr.normal("Installer part 1 is complete. Please reboot");
                        File.AppendAllText("/mnt/boot/host.wui/SYSTEMOBEE", "System is on obee currently");
                        UtilityLNX.halt();
                    }
                    catch
                    {
                        Console.Clear();
                        txtMgr.error("SYSTEM HALTED. Couldnt Setup your pc. Please restart and DO NOT REMOVE THE CD OR USB\nextra info: This may have happend because you didnt format your disk ");
                        UtilityLNX.halt();
                    }
                    break;
                case ConsoleKey.F3:
                    UtilityLNX.Format(1,"/mnt");
                    break;
                case ConsoleKey.F4:
                    try
                    {
                        txtMgr.normal("Starting partitioning...");
                        UtilityLNX.createpart();
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        txtMgr.warn("SYSTEM HALTED. could not partition your disk. Please restart and DO NOT REMOVE THE CD OR USB\n" + e.Message);
                        Power.Shutdown();
                    }
                    break;
                case ConsoleKey.F5:
                    File.AppendAllText("/mnt/LunDos.ini", $"username=Native_LN_COSMOS\n");
                    txtMgr.normal("Starting envoriment");
                    Console.BackgroundColor = ConsoleColor.Black;

                    return;
            }




        }
    }
    public static void part2()
    {
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();
        Console.Write("Type your name: ");
        string user = Console.ReadLine().Trim();
        File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", $"username={user}\n");
        Directory.CreateDirectory($"/mnt/Users/{user}");
        //password
        Console.Write("Type your password or just press enter: ");
        string pas = Console.ReadLine().Trim();
        File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", $"password={pas}\n");
        bool selection1 = true;

        Console.Clear();
        while (true)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal("====LNX OS INSTALLER===="); // second time i have to draw this
            Console.BackgroundColor = ConsoleColor.Blue;
            txtMgr.normal("To navigate through the menu Just press 1 or 2\n");
            txtMgr.normal("Do you want Gui on boot?");
            if (!selection1 == true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("Yes I want gui on boot");
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("No i dont want gui on boot");
                Console.BackgroundColor = ConsoleColor.Blue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selection1 = true;
                        break;
                    case ConsoleKey.Enter:
                        File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", "GuiOnBoot=false\n");
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
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        selection1 = false;
                        break;
                    case ConsoleKey.Enter:
                        File.AppendAllText("/mnt/LNXsys/System64/LunDos.ini", "GuiOnBoot=true\n");
                        part3();
                        break;
                }
            }

        }
    }
    public static void part3()
    {
        bool selection1 = true;
        while (true)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal("====LNX OS INSTALLER===="); //third. Like why???
            Console.BackgroundColor = ConsoleColor.Blue;
            txtMgr.normal("To navigate through the menu Just press 1 or 2\n");
            txtMgr.normal("What font would you want for gui");
            if (!selection1 == true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("Regular font");
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("Comic sans");
                Console.BackgroundColor = ConsoleColor.Blue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selection1 = true;
                        break;
                    case ConsoleKey.Enter:
                        File.AppendAllText("/mnt/LNXsys/system64/LunDos.ini", "font=ComicSans\nfontPath=/mnt/LNXsys/Fonts/lol.ttf\n");
                        part4();
                        break;

                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("Regular font");
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("Comic sans");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        selection1 = false;
                        break;
                    case ConsoleKey.Enter:
                        File.AppendAllText("/mnt/LNXsys/system64/LunDos.ini", "font=Reg\nfontPath=/mnt/LNXsys/Fonts/mainfont.ttf\n");
                        part4();
                        break;
                }
            }
        }
    }
    public static void part4()
    {
        bool selection1 = true;
        while (true)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal("====LNX OS INSTALLER====");
            Console.BackgroundColor = ConsoleColor.Blue;
            txtMgr.normal("To navigate through the menu Just press 1 or 2\n");
            txtMgr.normal("Do you want to reboot?");
            if (!selection1 == true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("Yes, please reboot");
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("No, just halt the computer");
                Console.BackgroundColor = ConsoleColor.Blue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selection1 = true;
                        break;
                    case ConsoleKey.Enter:
                        
                            File.Delete("/mnt/boot/host.wui/SYSTEMOBEE");
                            UtilityLNX.halt();
                    break;

                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                txtMgr.normal("Yes, please reboot");
                Console.BackgroundColor = ConsoleColor.Blue;
                txtMgr.normal("No, just halt the computer");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        selection1 = false;
                        break;
                    case ConsoleKey.Enter:
                        File.Delete("/mnt/boot/host.wui/SYSTEMOBEE");
                        Power.Reboot();
                        break;
                }
            }
        }

    }

    static public void RawInstall(byte[] File, ulong sect)
    {
        IBlockDevice? disk = StorageManager.PrimaryDevice;
        ulong sectorCount = (ulong)((File.Length + 511) / 512);

        if (File.Length % 512 != 0)
        {
            Array.Resize(ref File, (int)(sectorCount * 512));
        }


        disk.WriteBlock(sect, sectorCount, File);
    }
}