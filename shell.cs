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
using Cosmos.Network.Http;
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
using Cosmos.Kernel.Drivers.Usb.Storage;
using Cosmos.Kernel.HAL.DriverKit.Buses.Usb;
using Cosmos.Kernel.System.Audio;
using lnkrnl.win.gui;

namespace lnkrnl;

/// <summary>
/// Main kernel class - inherits from Cosmos.Kernel.System.Kernel.
/// </summary>
public class mainKrn
{

    static bool man = false;
    static string DefaultPartName = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "FsName");

    public static GuiData guiLauncher = new GuiData();
    static public void launch()
    {
        taskMgr.end("Minimal:subsystem:boot:syskern");
        taskMgr.init("lnoskrnl:mainKrn");
        //txtMgr.aprove("Succesfully launched after boot");


        if (VfsManager.TryStatFs("/mnt", out VfsStatFs stats))
        {
            ulong freeBytes = stats.FreeBlocks;

            ulong totalBytes = stats.AvailableBlocks;
            txtMgr.info($"{freeBytes} out of {totalBytes} bytes left on the main partition");
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        //txtMgr.customNL(ConsoleColor.DarkBlue,"KERNEL booted successfully!");
        if(boot.GetLimineArg("CMD"))
        {
            CliShell();
        }
        if (guionboot == "true")
        {
            txtMgr.aprove("Gui on boot is true");

            guiLauncher.IfSupportedInitGui();
        }
        CliShell();
    }
    static int line = 0;
    static string guionboot = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "GuiOnBoot");
    static public string username = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini", "username");
    static public void CliShell()
    {
        taskMgr.init("shell:mainKrn");
        taskMgr.init("users:shell:mainKrn");
        taskMgr.init("cmd:shell:mainKrn");

        while (true)
        {

            line++;

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
            string? input = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(input))
                continue;
            if (input.StartsWith("mkdir -p "))
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
                continue;
            }

            else if (input.StartsWith("lua "))
            {
                string file = input.Split(" ")[1].Trim();
                string arguments = input.Substring($"lua {file}".Length).Trim();
                Lua.Run(file, arguments);
                continue;
            }
            else if (input.StartsWith("cd "))
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
                continue;
            }
            else if (input.StartsWith("mv "))
            {
                string file = input.Split(" ")[1].Trim();
                string dest = input.Split(" ")[2];
                try
                {
                    File.Move(file,dest);
                }
                catch
                {
                    txtMgr.error("Not valid file to move");
                }
                continue;
            }
            else if (input.StartsWith("cp "))
            {
                string file = input.Split(" ")[1].Trim();
                string dest = input.Split(" ")[2];
                try
                {
                    File.Copy(file,dest);
                }
                catch
                {
                    txtMgr.error("Not valid file to copy");
                }
                continue;
            }
            else if (input.StartsWith("pika "))
            {
                string file = input.Split(" ")[1].Trim();
                try
                {
                    pik.driver.PIKwriter.Compile(file, $"{Directory.GetCurrentDirectory()}/out.pik");
                }
                catch(Exception e)
                {
                    txtMgr.error($"ERROR: {e}");
                }
                continue;
            }
            else if (input.StartsWith("pik "))
            {
                string file = input.Split(" ")[1].Trim();
                try
                {
                    pik.exec.run(file);
                }
                catch(Exception e)
                {
                    txtMgr.error($"ERROR: {e}");
                }
                continue;
            }
            else if (input.StartsWith("del "))
            {
                string sector = input.Split(" ")[1].Trim();
                try
                {
                    if (File.Exists(sector))
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
                continue;
            }
            else if (input.StartsWith("cat "))
            {
                string sector = input.Split(" ")[1].Trim();
                try
                {
                    Console.WriteLine(File.ReadAllText($"{Directory.GetCurrentDirectory()}/{sector}"));

                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"This isnt a valid file or it  doesnt exist");
                }
                continue;
            }
            else if (input.StartsWith("type "))
            {
                string filename = input.Split(" ")[1].Trim();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Type the text :");
                    string text = Console.ReadLine();
                    File.WriteAllText($"{Directory.GetCurrentDirectory()}/{filename}", text);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Couldnt make a new file");
                }
                continue;
            }
            else if (input.StartsWith("echo "))
            {
                string filename = input.Substring(5);
                Console.WriteLine(filename);
                continue;
            }
            else if (input.StartsWith("editor"))
            {
                try
                {
                    string filename = input.Substring(7);
                    editor.launch($"{Directory.GetCurrentDirectory()}/{filename}");
                    continue;
                }
                catch
                {
                    txtMgr.error("Failed to launch the editor");
                }

            }
            switch (input.ToLower())
            {

                case "reboot":
                    txtMgr.warn("rebooting...");
                    Power.Reboot();
                    break;
                case "shutdown":
                    UtilityLNX.Shutdown();
                    break;
                case "tasks":
                    taskMgr.List();
                    break;
                case "partinf":
                    if (VfsManager.TryStatFs("/mnt", out VfsStatFs stats1))
                    {
                        ulong avai = stats1.AvailableBlocks;
                        ulong blockSize = stats1.BlockSize;
                        txtMgr.info($"PARTITION 0 Avaible blocks:{avai}");
                        txtMgr.info($"PARTITION 0 Block size:{blockSize}");
                        txtMgr.info($"PARITITON 0 Free blocks:{stats1.FreeBlocks}");
                        txtMgr.info($"PARTITION 0 Mount:/mnt");
                    }
                    if (VfsManager.TryStatFs("/mntB", out VfsStatFs stats2))
                    {
                        ulong avai = stats2.AvailableBlocks;
                        ulong blockSize = stats2.BlockSize;
                        txtMgr.info($"PARTITION 1 Avaible blocks:{avai}");
                        txtMgr.info($"PARTITION 1 Block size:{blockSize}");
                        txtMgr.info($"PARITITON 1 Free blocks:{stats2.FreeBlocks}");
                        txtMgr.info($"PARTITION 1 Mount:/mntB");
                    }
                    break;
                case "audio":
                    if (!VfsManager.TryOpenFile("/mnt/LNXsys/System64/test.wav", out IVfsFileHandle? file))
                    {
                        Console.WriteLine("File not found");
                        continue;
                    }
                    if (!WaveAudioStream.TryOpen(file, out WaveAudioStream? song))
                    {
                        txtMgr.error("Not a PCM .wav file");
                        file.Dispose();
                        continue;
                    }
                    uint seconds = song.Length / song.SampleRate;
                    Console.WriteLine("Playing " + seconds + " s of audio");
                    if (!AudioManager.TryStartPlayback(song, file, out AudioPlayback? playback))
                    {
                        txtMgr.error("No output, the output is busy, or no scheduler to run the playback on");
                        file.Dispose();
                        continue;
                    }
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
                case "test":
                    ConsoleSession session = SessionManager.CreateVirtualConsole();

                    SessionManager.Start(session, () =>
                    {
                        SessionManager.Switch(session.Id);
                        Console.WriteLine("Hello from " + session.Name);

                        while (Console.ReadLine() is { } line)
                        {
                            Console.WriteLine("You typed: " + line);
                        }
                    });

                    break;
                case "testgui":
                    txtMgr.normal("Supported modes are:");
                    if (GuiData.HDIsSupported())
                    {
                        txtMgr.aprove("Gui is supported!");
                    }
                    break;
                case "gui":
                   
                    guiLauncher.IfSupportedInitGui();
                    break;
                case "date":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{DateTime.Now}");
                    break;
                case "format":
                    UtilityLNX.Format(1,"/mnt");
                    break;
                case "ext2":
                    // Newshell.Launch();
                    break;

                case "ls":
                    UtilityLNX.listfolder();
                    break;
                case "lnkrnl -check":
                    int errors = 0;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Checking LnxOS ");

                    if (File.Exists("/mnt/LNXsys/System64/LunDos.ini"))
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
                        errors = +1;

                    }
                    if (File.Exists("/mnt/LNXsys/System64/instLuviz.lze"))
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
                        errors = +1;
                    }
                    if (Directory.Exists("/mnt/LNXsys"))
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
                        errors = +1;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"There are: {errors}. \nIf theres any errors Please fix your installation");
                    break;
                case "re-setup":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();

                    Console.WriteLine("Please press any key to restart");

                    Console.ReadKey();
                    if (File.Exists("/mnt/LNXsys/System64/instLuviz.lze"))
                    {
                        if (Directory.Exists("/mnt/LNXsys/System64"))
                        {
                            if (File.Exists("/mnt/LNXsys/System64/LunDos.ini"))
                            {
                                File.Delete("/mnt/LNXsys/System64/LunDos.ini");
                            }
                            if (File.Exists("/mnt/LNXsys/System64/instLuviz.lze"))
                            {
                                File.Delete("/mnt/LNXsys/System64/instLuviz.lze");
                            }
                            if (File.Exists("/mnt/users/lnchck"))
                            {
                                File.Delete("/mnt/Users/lnchck");
                            }
                            if (File.Exists("/mnt/lnxOSpage.lua"))
                            {
                                File.Delete("/mnt/lnxOSpage.lua");
                            }
                            Directory.Delete("/mnt/LNXsys", true);
                        }





                    }
                    Power.Reboot();
                    break;
                case "help":
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Available commands:");
                    Console.WriteLine("  cat <filename> - Read a file and it displays its text");
                    Console.WriteLine("  type <filename> - Writes a file to the mounted partition");
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
                    Console.WriteLine("  lua <path> <Extra arguments>     - Lua interpreter");
                    Console.WriteLine("  editor <path>     - File editor");
                    Console.WriteLine("  luaenv     - Shows the lua envoriment");
                    Console.WriteLine("  partinf    - Gives partition info");
                    Console.WriteLine("  getargs    - Gets the args  that lnxos has booted with");
                    Console.WriteLine("  pika    - Gets the args  that lnxos has booted with");
                    Console.WriteLine("  mv    - moves a file to the directory specified (Also can work for renaming)");
                    Console.WriteLine("  cp    - copies a file to the directory specified");
                    Console.WriteLine("  pik    - runs a file using pik executable");
                    break;
                case "getargs":
                    var args = Environment.GetCommandLineArgs();
                    foreach(string arg in args)
                    {
                        txtMgr.normal(arg);
                    }
                    
                break;
                case "luaenv":
                    Lua.Environment();
                    break;

                case "ver":
                    Lua.Run("/mnt/LNXsys/bin/ver.lua", "");
                    break;

                case "abt":
                    Lua.Run("/mnt/LNXsys/bin/about.lua", "");
                    //Console.WriteLine("Made on: Gen 3 v3.0.88 COSMOS");
                    break;
                case "clear":
                    Console.Clear();
                    break;

                case "halt":
                    Console.WriteLine("Halting system...");
                    while (true) Power.Halt();

                default:
                    txtMgr.error($"Shell Failed at line:{line}: \"{input}\" is not a command");
                    break;

            }
        }



    }

}
