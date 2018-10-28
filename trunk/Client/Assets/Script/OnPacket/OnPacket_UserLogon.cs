using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script;
using UnityEngine;
using Assets.Script.Frame;

class OnPacket_UserLogon
{
    public static void OnPacket(object pack)
    {
        Debug.Log("OnPacket_UserLogon");
        UserLogon userLogon = pack as UserLogon;
        if (userLogon.ErrorCode == 0)
        {
            Debug.Log("Logon Succeed");
            Gamedata.me.userinfo = userLogon.UserInfo;
            PanelManager.me.Get((int)PanelId.PanelLogon).Hide();
            PanelManager.me.Get((int)PanelId.PanelMain).Show();
            Debug.Log(userLogon.UserInfo.HeroList.Count);
            Debug.Log(userLogon.UserInfo.HeroList[0].Id);
            Debug.Log(userLogon.UserInfo.HeroList[1].Id);
        }
        else if (userLogon.ErrorCode == 1)
        {
            Debug.Log("Logon Failed.");
        }
    }
}
