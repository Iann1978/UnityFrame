using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Frame
{
    class UiTextRegister : UiControlRegistor
    {
        public override void Awake()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiTextBehaviour(" + name + ").Awake");
            #endif
              Text text = GetComponent<Text>();
            container.RegistText(name, text);
        }

        public override void Destroy()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiTextBehaviour(" + name + ").Destroy");
#endif
            container.UnRegistText(name);
        }

    }
}
