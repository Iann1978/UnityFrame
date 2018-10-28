using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Frame;

namespace Assets.Script.OnPacket
{
    class OnPacket_ChangeUsername
    {
        public static void OnPacket(object pack)
        {
            Debug.Log("OnPacket_ChangeUsername");
            ChangeUsername changeUsername = pack as ChangeUsername;

            Gamedata.me.userinfo.Username = changeUsername.Username;
            PanelManager.me.RefreshAll();
        }
    }
}
