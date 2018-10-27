using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqLogon
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqLogon req = pack as ReqLogon;
            UserLogon userLogon = new UserLogon();
            userLogon.UserInfo = UserSystem.me.GetByUsernameAndPassword(req.Username, req.Password);
            userLogon.ErrorCode = userLogon.UserInfo != null ? 0 : 1;
            if (userLogon.ErrorCode == 0)
            {
                userLogon.UserInfo.HeroList = HeroSystem.me.AllHeros(userLogon.UserInfo.Id);

            }
            client.Send(userLogon);
            return;

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
