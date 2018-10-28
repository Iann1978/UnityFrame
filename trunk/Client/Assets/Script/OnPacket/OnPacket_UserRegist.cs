using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Frame;

namespace Assets.Script.OnPacket
{
    class OnPacket_UserRegist
    {
        public static void OnPacket(object pack)
        {
            Debug.Log("OnPacket_UserRegist");
            UserRegist userRegist = pack as UserRegist;

            if (userRegist.ErrorCode == 0)
            {
                Debug.Log("Regist Succeed");
                Gamedata.me.userinfo = userRegist.UserInfo;
                PanelManager.me.Get((int)PanelId.PanelLogon).Hide();
                PanelManager.me.Get((int)PanelId.PanelSetting).Show();
            }
            else if (userRegist.ErrorCode == 1)
            {
                Debug.Log("Username has already exist.");
            }            
        }
    }
}
