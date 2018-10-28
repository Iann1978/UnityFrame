
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Frame;

namespace Assets.Script.OnPacket
{
    class OnPacket_TeamIconFrameChanged
    {
        public static void OnPacket(object pack)
        {
            Debug.Log("OnPacket_TeamIconFrameChanged");
            TeamIconFrameChanged rsp = pack as TeamIconFrameChanged;

            Gamedata.me.userinfo.TeamIconFrame = rsp.TeamIconFrame;
            PanelManager.me.Get((int)PanelId.PanelTeamInfo).Refresh();





        }
    }
}
