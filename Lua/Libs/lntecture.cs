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
using System.Security.AccessControl;
using lnkrnl.win.gui;
namespace lnkrnl;
public class LuaLibs
{
    public static lunwm dwm = new lunwm();
    public static void add(ILuaState inter, CSharpFunctionDelegate function, string name)
    {
        inter.PushCSharpFunction(function);
        inter.SetGlobal(name);
    }
    public static void ActualInt(LuaInterpreter lua)
    {
        var state = lua.State;
        add(state,wait,"ln_wait");
        add(state,backroundTSK,"task_init");
        add(state,MakeNewTEXTWindow,"ln_lusd_win_generate");
        add(state,listTSK,"task_list");
        add(state,endTSK,"task_end");
        add(state,info,"info");
        add(state,approve,"yes");
        add(state,error,"error");
        add(state,clear,"ln_clear");
        add(state,listfolder,"ln_ls");
        add(state,WINMGRCREATE,"lunwm_create");
    }
    public static int WINMGRCREATE(ILuaState luaSTS)
    {
        var name = luaSTS.L_CheckString(1);
        var width = luaSTS.L_CheckInteger(2);
        var height = luaSTS.L_CheckInteger(3);
        dwm.Generate(Desktop.rect,(int)width,(int)height,name);
        return 0;

    }
    public static int listfolder(ILuaState luaSTS)
    {
        UtilityLNX.listfolder(); // utillitylnx.listfolder()for this case
        return 0;
    }
    
    public static int template(ILuaState luaSTS)
    {
        return 0;
    }
    public static int delete(ILuaState luaSTS)
    {
        var name = luaSTS.L_CheckString(1);
        File.Delete(name);
        return 0;
    }

    public static int MakeNewTEXTWindow(ILuaState luaSTS)
    {   
       
        if(luaSTS.L_CheckInteger(2) == 1)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal(luaSTS.L_CheckString(1));
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            txtMgr.normal(luaSTS.L_CheckString(1));
            Console.BackgroundColor = ConsoleColor.Black;
        }

        return 0;
        
       
    }
    public static int clear(ILuaState luaSTS)
    {
        Console.Clear();
        return 0;
    } 
    public static int error(ILuaState luaSTS)
    {
        var text = luaSTS.L_CheckString(1);
        txtMgr.error(text);
        return 0;
    }
    public static int approve(ILuaState luaSTS)
    {
        var text = luaSTS.L_CheckString(1);
        txtMgr.aprove(text);
        return 0;
    }
    public static int info(ILuaState luaSTS)
    {
        var text = luaSTS.L_CheckString(1);
        txtMgr.info(text);
        return 0;
    }
    public static int endTSK(ILuaState luaSTS)
    {
        var taskname = luaSTS.L_CheckString(1);
        taskMgr.end(taskname);
        return 0;
    }
    public static int listTSK(ILuaState luaSTS)
    {
        taskMgr.List();
        return 0;
    }
    public static int backroundTSK(ILuaState luaSTS)
    {
        var taskname = luaSTS.L_CheckString(1);
        taskMgr.init(taskname + ":Lua");
        return 0;
    }
        public static int wait(ILuaState luaSTS)
    {
        var secs = luaSTS.L_CheckNumber(1);
        Thread.Sleep((int) secs * 1000);
        return 0;
    }
}