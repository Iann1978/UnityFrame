﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Assets_Script_Frame_Singleton_GamedataWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Assets.Script.Frame.Singleton<Gamedata>), typeof(System.Object), "Singleton_Gamedata");
		L.RegFunction("New", _CreateAssets_Script_Frame_Singleton_Gamedata);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("me", get_me, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAssets_Script_Frame_Singleton_Gamedata(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Assets.Script.Frame.Singleton<Gamedata> obj = new Assets.Script.Frame.Singleton<Gamedata>();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Assets.Script.Frame.Singleton<Gamedata>.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_me(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, Assets.Script.Frame.Singleton<Gamedata>.me);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

