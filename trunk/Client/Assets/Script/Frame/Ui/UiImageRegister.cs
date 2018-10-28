using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Frame
{
    class UiImageRegister : UiControlRegistor
    {
        public override void Awake()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiImageBehaviour(" + name + ").Awake");
            #endif
            Image img = GetComponent<Image>();
            container.RegistImage(name, img);
        }

        public override void Destroy()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiImageBehaviour(" + name + ").Destroy");
            #endif
            container.UnRegistImage(name);
        }

    }
}
