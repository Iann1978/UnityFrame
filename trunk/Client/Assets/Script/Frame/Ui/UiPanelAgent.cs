using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Frame
{
    class UiPanelAgent : UiControlRegistor
    {
        public PanelBase prefab;
        public override void Awake()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiPanelAgent(" + name + ").Awake");
            #endif
            PanelBase subPanel = Instantiate<PanelBase>(prefab);
            subPanel.transform.parent = transform.parent;            
            subPanel.transform.localPosition = transform.localPosition;
            subPanel.transform.localScale = Vector3.one;
            container.RegistPanelBase(prefab.name, subPanel);
        }

        public override void Destroy()
        {
            #if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiPanelAgent(" + name + ").Destroy");
            #endif
            container.UnRegistPanelBase(name);
        }

    }
}
