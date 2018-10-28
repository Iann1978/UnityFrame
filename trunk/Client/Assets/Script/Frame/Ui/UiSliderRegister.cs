

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Frame
{
    class UiSliderRegister : UiControlRegistor
    {
        public override void Awake()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiSliderBehaviour(" + name + ").Awake");
            #endif
            Slider slid = GetComponent<Slider>();
            container.RegistSlider(name, slid);
        }

        public override void Destroy()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiSliderBehaviour(" + name + ").Destroy");
            #endif
            container.UnRegistSlider(name);
        }

    }
}
