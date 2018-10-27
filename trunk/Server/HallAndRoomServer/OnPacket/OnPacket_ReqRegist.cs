using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqRegist
    {
        public static void OnPacket(Client client, object pack)
        { 
            ReqRegist req = pack as ReqRegist;
            UserRegist rsp = new UserRegist();

            UserInfo userInfo = UserSystem.me.GetByUsername(req.Username);
            if (userInfo != null)
            {
                rsp.ErrorCode = 1;
                client.Send(rsp);
                return;
            }


            userInfo = UserSystem.me.NewUser(req.Username, req.Password);
            HeroSystem.me.NewHero(userInfo.Id, 2);
            HeroSystem.me.NewHero(userInfo.Id, 3);
            userInfo.HeroList = HeroSystem.me.AllHeros(userInfo.Id);
            rsp.ErrorCode = 0;
            rsp.UserInfo = userInfo;
            client.Send(rsp);

            //string cmd = String.Format("select * from user where Username='{0}'", req.Username);
            //MySqlDataReader reader = Database.me.ExecQuery(cmd);
            //if (reader.Read())
            //{
            //    reader.Close();
            //    rsp.ErrorCode = 1;
            //    client.Send(rsp);
            //    return;
            //}


            ////reader.Close();
            //string cmd = String.Format("INSERT INTO `user`(`UserName`, `Password`, `Coin`) VALUES ('{0}','{1}',10)", req.Username, req.Password);
            //Database.me.ExecNonQuery(cmd);


            //cmd = String.Format("select * from user where Username='{0}'", req.Username);
            //MySqlDataReader reader = Database.me.ExecQuery(cmd);
            //if (reader.Read())
            //{
            //    rsp.ErrorCode = 0;
            //    rsp.UserInfo = new UserInfo();
            //    rsp.UserInfo.Id = reader.GetInt32("Id");
            //    rsp.UserInfo.Username = reader.GetString("Username");
            //    rsp.UserInfo.Coin = reader.GetInt32("Coin");
            //    reader.Close();
            //    client.Send(rsp);
            //}
        }
    }
}
