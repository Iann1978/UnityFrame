﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class HeroItemWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(HeroItem), typeof(Assets.Script.Frame.PanelBase));
		L.RegFunction("GetControls", GetControls);
		L.RegFunction("SetHeroInfo", SetHeroInfo);
		L.RegFunction("SetHeroData", SetHeroData);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetControls(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			HeroItem obj = (HeroItem)ToLua.CheckObject(L, 1, typeof(HeroItem));
			obj.GetControls();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetHeroInfo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			HeroItem obj = (HeroItem)ToLua.CheckObject(L, 1, typeof(HeroItem));
			HeroInfo arg0 = (HeroInfo)ToLua.CheckObject(L, 2, typeof(HeroInfo));
			obj.SetHeroInfo(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetHeroData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			HeroItem obj = (HeroItem)ToLua.CheckObject(L, 1, typeof(HeroItem));
			HeroTableData arg0 = (HeroTableData)ToLua.CheckObject(L, 2, typeof(HeroTableData));
			obj.SetHeroData(arg0);
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
}

