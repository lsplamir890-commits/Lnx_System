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
namespace lnkrnl.win.gui;
public class Desktop
{   
  
     static public Png warn = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.warn.png"));
    static public Png rect = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.rectangle.png"));
    static public Png applogo = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.logo.png"));
    static public Png pfp = new Png(EmbeddedResource.ReadBytes("lnkrnl.resources.pfp.png"));
    static public string fontPath = UtilityLNX.ReadIni("/mnt/LNXsys/System64/LunDos.ini","fontPath");
    static public Png cursor = new Png("/mnt/LNXsys/System64/cursor.png");
    static public Png icon = new Png("/mnt/LNXsys/System64/BootIcon.png");
    static public TrueTypeFont fnt = new TrueTypeFont(fontPath, 16);
    static public Png deskImg = new Png("/mnt/LNXsys/themes/nebula.png");
    static public int YFilesPoss = 109; 
    static public string username = UtilityLNX.ReadIni("/mnt/LnxSys/System64/LunDos.ini", "username");
    
    //public static Widgets widgets = new Widgets();
    public static void init(Canvas canvas)
    {

        
        try
        {
            
           
            canvas.Clear(Color.Red);
            canvas.DrawImage(deskImg, 0, 0);
            foreach(var file in Directory.GetFiles($"/mnt/Users/{mainKrn.username}/Desktop"))
            {
                string contents = File.ReadAllText(file);
                canvas.DrawFilledRectangle(Color.Blue,45,YFilesPoss,  71 ,488);
                canvas.DrawString(file,fnt,Color.White,45,YFilesPoss + 60) ;
                if(GuiApi.PressedArea(45,YFilesPoss,488,71))
                {
                    Lua.Run(file,"");
                }
                YFilesPoss += 100;
                
                
            }
            //new Window.Wim().window(500,500,"test window",rect, canvas);
            // widgets.Button(0,0,100,50, ()=> 
            //     Power.Reboot()
            // );
        } catch(Exception e)
        {
            UtilityLNX.Panic($"{e.Message}","0x5");
        }

        
        
           
    }
}