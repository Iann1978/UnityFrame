using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace HallAndRoomServer
{
    class UserSystem : Singleton<UserSystem>
    {
        public static UserInfo ReaderToUserInfo(MySqlDataReader reader)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.Id = reader.GetInt32("Id");
            userInfo.Username = reader.GetString("Username");
            userInfo.Coin = reader.GetInt32("Coin");
            userInfo.Diamond = reader.GetInt32("Diamond");
            userInfo.Vigour = reader.GetInt32("Vigour");
            userInfo.VipLevel = reader.GetInt32("VipLevel");
            userInfo.TeamLevel = reader.GetInt32("TeamLevel");
            userInfo.TeamExp = reader.GetInt32("TeamExp");
            userInfo.TeamIcon = reader.GetInt32("TeamIcon");
            userInfo.TeamIconFrame = reader.GetInt32("TeamIconFrame");
            userInfo.BattlePower = reader.GetInt32("BattlePower");
            return userInfo;
        }

        public UserInfo NewUser(string username, string password)
        {
            UserInfo userInfo = new UserInfo();
            string cmd = String.Format("INSERT INTO `user`(`UserName`, `Password`, `Coin`) VALUES ('{0}','{1}',10)", username, password);
            Database.me.ExecNonQuery(cmd);
            return GetByUsernameAndPassword(username, password);
        }

        public UserInfo GetById(int id)
        {
            UserInfo userInfo = null;
            string cmd = String.Format("SELECT * FROM `user` WHERE Id={0}", id.ToString());
            MySqlDataReader reader = Database.me.ExecQuery(cmd);
            if (reader.Read())
            {
                userInfo = ReaderToUserInfo(reader);
            }
            reader.Close();
            return userInfo;
        }

        public UserInfo GetByUsernameAndPassword(string username, string password)
        {
            UserInfo userInfo = null;
            string cmd = String.Format("SELECT * FROM `user` WHERE Username='{0}' and Password='{1}'", username, password);
            MySqlDataReader reader = Database.me.ExecQuery(cmd);
            if (reader.Read())
            {
                userInfo = ReaderToUserInfo(reader);
            }
            reader.Close();
            return userInfo;
        }

        public UserInfo GetByUsername(string username)
        {
            UserInfo userInfo = null;
            string cmd = String.Format("SELECT * FROM `user` WHERE Username='{0}'", username);
            MySqlDataReader reader = Database.me.ExecQuery(cmd);
            if (reader.Read())
            {
                userInfo = ReaderToUserInfo(reader);
            }
            reader.Close();
            return userInfo;
        }

        public bool ChangeTeamIconFrame(int userid, int teamIconFrame)
        {
            string cmd = String.Format("UPDATE user set TeamiconFrame={0} where Id={1}", teamIconFrame, userid);
            Database.me.ExecNonQuery(cmd);
            return true;
        }

        public bool ChangeUsername(int userid, string username)
        {
            string cmd = String.Format("Update user set Username='{0}' where Id={1}", username, userid);
            Database.me.ExecNonQuery(cmd);
            return true;
        }
    }
}
