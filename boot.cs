using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;


using Cosmos.Kernel.System.FileSystem.Fat;
using Cosmos.Kernel.System.FileSystem.Iso9660;
using Cosmos.Kernel.HAL.DriverKit;
using Cosmos.Kernel.System.Sessions;
using Cosmos.Kernel.System;
using Cosmos.Kernel.HAL.Devices;
using Cosmos.Kernel.System.Diagnostics;
using Cosmos.Network.Telnet;
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
using Cosmos.Kernel.Boot.Limine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Cosmos.Kernel.System.Network;
using Cosmos.Kernel.System.Timers;

using lnkrnl.win.gui;
namespace lnkrnl;
public class boot : Sys.Kernel
{   
    

    public static bool GetLimineArg(string target)
    {
            var args = Environment.GetCommandLineArgs();
            return args.FirstOrDefault(a => a == target) != null;
    } 
   
     static public string username = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "username");
    protected override void BeforeRun()
    {
        
        KernelConsole.Default.Font = PCScreenFont.LoadFont(EmbeddedResource.ReadBytes("lnkrnl.resources.dosFont.psf"));
        taskMgr.init("boot:syskern");
        ConsoleSession terminal = SessionManager.CreateVirtualConsole();
        SessionManager.Start(terminal, ()=>
            txtMgr.info("Started kernel")
        );

        taskMgr.init("subsystem:boot:syskern");
        txtMgr.warn($"Time right now is:{DateTime.Now}");
        txtMgr.aprove("Started the pre-kernel with no problems yet");
        txtMgr.warn("This is a beta");
        txtMgr.info("Starting load sequence");
        taskMgr.init("Minimal:subsystem:boot:syskern");

        if(GetLimineArg("smD"))
        {
            txtMgr.aprove("SAFE MODE");
            if(Power.IsSupported == true)
            {
                SafeKrn.launch();
                txtMgr.error("SHUTDOWN ISNT SUPPORTED");
                Power.Shutdown();
            }
        }
        txtMgr.warn($"the kernel terminal is at {terminal.Id}");

        taskMgr.init("disk_service");
        
        txtMgr.info("Loading disk");
        
        taskMgr.init("fat");

        IBlockDevice? disk = StorageManager.PrimaryDevice;
        FatFileSystemType fat = new();
        
        if (!VfsManager.RegisterFileSystem("fat", fat))
        {
            txtMgr.warn("The name \"fat\" is already registered.");
            return;
        }

        txtMgr.info("rescanning partitions...");
        StorageManager.RescanPartitions(disk);
        txtMgr.aprove("scanned partitions");
        txtMgr.info($"The target drive is: {disk.Name}");
        if (StorageManager.Partitions.Count < 2)
        {
            
            Console.WriteLine("No partitions found.");
            try
            {
                txtMgr.error("COULDNT FIND ANY PARTITIONS");
                txtMgr.info("press any key to create an partition and reboot");
                Console.ReadKey();
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
        if (VfsManager.TryMount("fat", StorageManager.Partitions[1], MountFlags.None, "/mnt", out VfsMount? mount))
        {
            taskMgr.init(disk.Name);
            txtMgr.aprove("Mounted " + mount.Name + " at " + mount.MountPoint);
        }
        foreach(Partition part in StorageManager.GetPartitions(disk) )
        {
            
        }
        if (VfsManager.TryMount("fat", StorageManager.Partitions[0], MountFlags.ReadOnly, "/efi", out VfsMount? efiMount))
        {
            txtMgr.aprove("Mounted " + efiMount.Name + " at " + efiMount.MountPoint);
        }
        else
        {
            UtilityLNX.createpart();
        }
        if(GetLimineArg("dev"))
        {
            
            InterfaceDevice.InitDesktopDriver();
            Guinvas.init();
        }
        // if (!VfsManager.TryMount("fat", StorageManager.Partitions[1], MountFlags.None, "/mntB", out VfsMount? mount1))
        // {
            
        // }
        if(GetLimineArg("cdmode"))
        {
            KernelConsole.Default.Font = PCScreenFont.DefaultFont;
            InstallService.part1();
        }
        if (NetworkManager.DeviceCount > 0)
        {
            Console.WriteLine("Device:  " + NetworkManager.Name);
            Console.WriteLine("MAC:     " + NetworkManager.MacAddress?.ToString());
            Console.WriteLine("Link up: " + NetworkManager.LinkUp);
            Console.WriteLine("Ready:   " + NetworkManager.Ready);
        }

        NetworkAdapter second = NetworkManager.GetAdapter(0);
        txtMgr.aprove($"{second.IPConfig}");
        DhcpClient dhcpClient = new();
        IPConfig? config = NetworkManager.Primary.IPConfig;
        //Console.WriteLine("IP address: " + config.Address.ToString());
        if (dhcpClient.SendDiscoverPacket() != -1)
        {
            //IPConfig? config = NetworkManager.Primary.IPConfig;
            if (config is not null)
            {
                Console.WriteLine("IP address: " + config.Address.ToString());
                Console.WriteLine("Subnet:     " + config.SubnetMask.ToString());
                Console.WriteLine("Gateway:    " + config.DefaultGateway.ToString());
            }
        }
        else
        {
            Console.WriteLine("DHCP timed out");
        }

        // if(!File.Exists("/mnt/LNXsys/System64/instLuviz.lze") && !File.Exists("/mnt/lnxOSpage.lua") && !File.Exists("/mnt/Users/lnchck") && !File.Exists("/mnt/EFI/BOOT/BOOTX64.efi"))
        // {
        //     KernelConsole.Default.Font = PCScreenFont.DefaultFont;
        //     InstallService.part1();

        // }
        if(File.Exists("/mnt/boot/host.wui/SYSTEMOBEE"))
        {
            KernelConsole.Default.Font = PCScreenFont.DefaultFont;
            InstallService.part2();
        }
        TelnetServer server = new(session =>
        {
            txtMgr.aprove("TELNET COMMUNICATING TO SESSION " + session.Name);

            txtMgr.info("YOU WILL BE PROMPTED FOR A PASSWORD SHORTLY");
            UtilityLNX.Chckpass();
            txtMgr.info("SHELL EXECUTING...");
            mainKrn.launch();
        });
        server.Start();
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
            txtMgr.error("Couldnt set the current directory to Primary partition");
        }
        
        UtilityLNX.Chckpass();
        txtMgr.aprove("Succesfully loaded with no problems");
        Console.Beep();
        
        
        Thread.Sleep(1500);
        mainKrn.launch();

    }
    protected override void Run()
    {

    }
}