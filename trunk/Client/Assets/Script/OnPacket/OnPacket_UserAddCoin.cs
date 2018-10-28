using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Frame;
namespace Assets.Script.OnPacket
{
    class OnPacket_UserAddCoin
    {
        public static void OnPacket(object pack)
        {
            Debug.Log("OnPacket_UserAddCoin");
            UserAddCoin userAddCoin = pack as UserAddCoin;
           
               
                Gamedata.me.userinfo = userAddCoin.UserInfo;
                
                PanelManager.me.Get((int)PanelId.PanelGetMoney).Show();
          
        }
    }
}
