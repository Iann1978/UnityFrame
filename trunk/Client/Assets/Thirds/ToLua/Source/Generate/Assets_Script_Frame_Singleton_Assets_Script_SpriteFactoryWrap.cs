﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Assets_Script_Frame_Singleton_Assets_Script_SpriteFactoryWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Assets.Script.Frame.Singleton<Assets.Script.SpriteFactory>), typeof(System.Object), "Singleton_Assets_Script_SpriteFactory");
		L.RegFunction("New", _CreateAssets_Script_Frame_Singleton_Assets_Script_SpriteFactory);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("me", get_me, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAssets_Script_Frame_Singleton_Assets_Script_SpriteFactory(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Assets.Script.Frame.Singleton<Assets.Script.SpriteFactory> obj = new Assets.Script.Frame.Singleton<Assets.Script.SpriteFactory>();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Assets.Script.Frame.Singleton<Assets.Script.SpriteFactory>.New");
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
			ToLua.PushObject(L, Assets.Script.Frame.Singleton<Assets.Script.SpriteFactory>.me);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

