//Dev of lnxos 1
using System;
using Sys = Cosmos.Kernel.System;
using System.IO;
using Cosmos.Kernel.System.Storage;

using Cosmos.Network.Telnet;
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
using PikExt;
using PikExt.code;
using lnkrnl.win;
using lnkrnl.lua;
namespace Cosmos.Native;
public partial class Library
{
    [LibraryImport("*",EntryPoint = "Buffer_Cnt")]
    public static partial int Buffer_Cnt();
    
    [LibraryImport("*",EntryPoint = "Kernel_C")]
    public static partial int Kernel_C(int init);
}