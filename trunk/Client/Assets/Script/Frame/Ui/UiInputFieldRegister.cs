using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Frame
{
    class UiInputFieldRegister : UiControlRegistor
    {
        public override void Awake()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiInputFieldBehaviour(" + name + ").Awake");
            #endif
            InputField inputFiled = GetComponent<InputField>();
            container.RegistInputField(name, inputFiled);
        }

        public override void Destroy()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiInputFieldBehaviour(" + name + ").Destroy");
            #endif
            container.UnRegistInputField(name);
        }
        
    }
}
