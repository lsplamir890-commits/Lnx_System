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
using System.Text;
namespace pik.driver;
//driver for pik compiler made 2026 and will be changed surely 
//PIKA compiler code
//made by Jsplashh
//this was hard
//
// NOT FINISHED!!!!!!!!!!!
public class PIKwriter
{
    static public byte[] magic = [0xDD, 0xED, 0xFf, ..Encoding.ASCII.GetBytes(byteHelper.Version),0xFd,0xEA];
    static public void Compile(string path, string outpath)
    {
        File.AppendAllBytes(outpath, magic); //writes the header
        // pik extensions

        //we start compiler
        if(UtilityLNX.FindIniKey(path,"print"))
        {
            string printT = UtilityLNX.ReadIni(path,"printf");
            File.AppendAllBytes(outpath, byteHelper.print(printT)); //we write the bytes
        }        

        if(UtilityLNX.FindIniKey(path,"halt"))
        {
            File.AppendAllBytes(outpath,byteHelper.halt);
        }
        txtMgr.aprove($"Succesfully compiled file {path} using pika compiler");

    }
}