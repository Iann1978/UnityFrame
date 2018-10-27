using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqUserAddCoin
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqUserAddCoin req = pack as ReqUserAddCoin;
            UserAddCoin userAddCoin = new UserAddCoin();
            UserInfo userInfo = UserSystem.me.GetById(req.Userid);
            if (userInfo != null)
            {
               
                client.Send(userAddCoin);
                return;
            }
            userAddCoin.UserInfo = userInfo;
            client.Send(userAddCoin);

            //string cmd = String.Format("SELECT * FROM `user` WHERE Username='{0}'", "test0");
            //MySqlDataReader reader = Database.me.ExecQuery(cmd);
            //if (reader.Read())
            ////if (reader.NextResult())
            //{
            //    client.SetStatus(Client.Status.InHall);
            //    client.userid = ++Client.userIdCounter;
            //    UserLogon userLogon = new UserLogon();
            //    userLogon.UserId = client.userid;
            //    userLogon.UserInfo = new UserInfo();
            //    userLogon.UserInfo.Id = 0;
            //    userLogon.UserInfo.Username = reader.GetString("Username");
            //    userLogon.UserInfo.Coin = 11;
            //    userLogon.UserInfo.Diamond = 12;
            //    userLogon.UserInfo.Vigour = 13;
            //    userLogon.UserInfo.VipLevel = 14;
            //    userLogon.UserInfo.TeamLevel= 15;
            //    userLogon.UserInfo.BattlePower = 16;


            //    userLogon.UserInfo.Coin = 12;

            //    client.Send(userLogon);
            //}
            //reader.Close();


        }
    }
}
