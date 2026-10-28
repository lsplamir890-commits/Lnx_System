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
using pik.driver;
using System.Text;
namespace pik;
public class exec
{
    static public void run(string path, bool userspace = true)
    {
        
        bool StringOpen = false;
        List<byte> StringText = new List<byte> {};
        byte[] file = File.ReadAllBytes(path);
        if(!file.Take(PIKwriter.magic.Length).SequenceEqual(PIKwriter.magic))
        {
            throw new Exception("NOT_VALID_PIK_HEADER");
        }
        int skipB = PIKwriter.magic.Length;
        foreach(byte bm in file.Skip(skipB))
        {
            
            if(StringOpen)
            {
                if(bm == 0xEE)
                {
                    txtMgr.normal(Encoding.ASCII.GetString(StringText.ToArray()));
                    StringOpen = false;
                    StringText.Clear();
                    continue;
                }
                StringText.Add(bm);
                continue;
            }

            
            if(bm == 0xEF)
            {
                int start = Array.IndexOf(file,(byte)0xEF);
                StringOpen = true;
                continue;
            }
            else if(bm == 0xE0)
            {
                UtilityLNX.halt();
                continue;
            }
            else
            {
                throw new Exception("NO_VALID_INSTRUCCION");
                continue;
            }
        }
    }
}