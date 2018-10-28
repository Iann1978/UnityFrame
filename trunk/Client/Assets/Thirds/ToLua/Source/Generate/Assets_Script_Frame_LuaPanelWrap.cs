﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Assets_Script_Frame_LuaPanelWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Assets.Script.Frame.LuaPanel), typeof(Assets.Script.Frame.PanelBase));
		L.RegFunction("Init", Init);
		L.RegFunction("Refresh", Refresh);
		L.RegFunction("IsExistFunc", IsExistFunc);
		L.RegFunction("Show", Show);
		L.RegFunction("Hide", Hide);
		L.RegFunction("Call", Call);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("luafile", get_luafile, set_luafile);
		L.RegVar("self", get_self, set_self);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)ToLua.CheckObject(L, 1, typeof(Assets.Script.Frame.LuaPanel));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)ToLua.CheckObject(L, 1, typeof(Assets.Script.Frame.LuaPanel));
			obj.Refresh();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsExistFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)ToLua.CheckObject(L, 1, typeof(Assets.Script.Frame.LuaPanel));
			string arg0 = ToLua.CheckString(L, 2);
			bool o = obj.IsExistFunc(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Show(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)ToLua.CheckObject(L, 1, typeof(Assets.Script.Frame.LuaPanel));
			obj.Show();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Hide(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)ToLua.CheckObject(L, 1, typeof(Assets.Script.Frame.LuaPanel));
			obj.Hide();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Call(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)ToLua.CheckObject(L, 1, typeof(Assets.Script.Frame.LuaPanel));
			string arg0 = ToLua.CheckString(L, 2);
			obj.Call(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luafile(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)o;
			string ret = obj.luafile;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index luafile on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_self(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)o;
			object ret = obj.self;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index self on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luafile(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.luafile = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index luafile on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_self(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Assets.Script.Frame.LuaPanel obj = (Assets.Script.Frame.LuaPanel)o;
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.self = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index self on a nil value" : e.Message);
		}
	}
}
