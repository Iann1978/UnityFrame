﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ReqLogonWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ReqLogon), typeof(System.Object));
		L.RegFunction("New", _CreateReqLogon);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Username", get_Username, set_Username);
		L.RegVar("Password", get_Password, set_Password);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateReqLogon(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				ReqLogon obj = new ReqLogon();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: ReqLogon.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Username(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ReqLogon obj = (ReqLogon)o;
			string ret = obj.Username;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Username on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Password(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ReqLogon obj = (ReqLogon)o;
			string ret = obj.Password;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Password on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Username(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ReqLogon obj = (ReqLogon)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.Username = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Username on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Password(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ReqLogon obj = (ReqLogon)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.Password = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Password on a nil value" : e.Message);
		}
	}
}
