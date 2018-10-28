using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Assets.Script.Frame
{
    class UiButtonRegister : UiControlRegistor
    {        
        public enum Operation
        {
            None,
            Regist,
            SetDefaultClickFunction,
            SetGivenClickFunction,
            RegistAndSetDefaultClickFunction,
            RegistAndSetGivenClickFunction
        }
        public Operation oper = Operation.Regist;
        public string clickFunction = null;

        void Regist()
        {
            Button btn = GetComponent<Button>();
            container.RegistButton(name, btn);
        }

        void SetDefaultClickFunction()
        {
            string defaultClickFunctionName = "On" + name + "Click";
            SetClickFunction(defaultClickFunctionName);
        }

        void SetGivenClickFunction()
        {
            SetClickFunction(clickFunction);
        }

        void SetClickFunction(string funcName, bool withParam = false)
        {
            
            Button btn = GetComponent<Button>();
            LuaPanel luaPanel = container.GetComponent<LuaPanel>();
            if (luaPanel)
            {
                btn.onClick.AddListener(delegate () { luaPanel.Call(funcName); });
            }
            else
            {
                PanelBase panel = container.GetComponent<PanelBase>();
                MethodInfo info = panel.GetType().GetMethod(funcName);
                if (info != null)
                {
                    object[] param = withParam ? new object[] { gameObject } : null;
                    btn.onClick.AddListener(delegate () { info.Invoke(panel, param); });
                }
                else
                {
                    Debug.LogWarning(string.Format("The function {0} is hasn't been found in {1}.", funcName, name));
                }
            }

        }
        public override void Awake()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiButtonBehaviour(" + name + ").Awake");
            #endif
            if (oper == Operation.Regist || 
                oper == Operation.RegistAndSetDefaultClickFunction ||
                oper == Operation.RegistAndSetGivenClickFunction)
            {
                Regist();
            }

            if (oper == Operation.SetDefaultClickFunction ||
                oper == Operation.RegistAndSetDefaultClickFunction)
            {
                SetDefaultClickFunction();
            }

            if (oper == Operation.SetGivenClickFunction ||
                oper == Operation.RegistAndSetGivenClickFunction)
            {
                SetGivenClickFunction();
            }

            
        }


        public override void Destroy()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiButtonBehaviour(" + name + ").Destroy");
            #endif
            container.UnRegistButton(name);
        }
        
    }
}
