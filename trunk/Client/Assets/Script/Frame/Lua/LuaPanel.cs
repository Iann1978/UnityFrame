using UnityEngine;
using System.Collections;
using LuaInterface;
using System;

namespace  Assets.Script.Frame
{

    public class LuaPanel : PanelBase
    {
        public string luafile;
        LuaState lua = null;
        public object self;

        public override void Init()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("LuaPanel(" + name + ").Init");
#endif
            //base.Init();
            if (lua == null)
            {
                InitLua();
            }

            Call("Init");

            //string funcName = "Init";

            //string funcFullname = name + "." + funcName;
            //Debug.Log("LuaPanel.Call(" + funcFullname + ")");
            //LuaFunction func = lua.GetFunction(funcFullname);
            //if (func != null)
            //{
            //    func.Call(self);
            //}

        }

        public override void Refresh()
        {
            Call("Refresh");
        }

        public virtual bool IsExistFunc(string funcFullname)
        {
            LuaFunction func = lua.GetFunction(funcFullname);
            return func != null;
        }
        

        void InitLua()
        {
            lua = LuaSystem.me.lua;
            //lua.DoFile("luab/" + name);
            lua.DoFile(name);

            string ctorFuncName = name + ".New";
            LuaFunction func = lua.GetFunction(ctorFuncName);
            func.BeginPCall();
            func.Push(this);
            func.PCall();
            self = func.CheckVariant();
            func.EndPCall();
        }


        public override void Show()
        {
            base.Show();
            foreach(PanelBase subPanel in subPanels)
            {
                subPanel.Show();
            }


        }

        //public override void Refresh()
        //{
        //    //Call("OnShow");
        //    Call("Refresh");
        //}
        public override void Hide()
        {
            Debug.Log("Hide");
            gameObject.SetActive(false);
        }

        public void Call(string funcName)
        {
            string funcFullname = name + "." + funcName;

#if USE_LOG && LOG_FRAME_UI
            Debug.Log("LuaPanel:Call(" + funcFullname + ")");
#endif

            LuaFunction func = lua.GetFunction(funcFullname);
            if (func != null)
            {
                func.Call(self);
            }
            
        }

    }

}
