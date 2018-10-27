
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqChangeUsername
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqChangeUsername req = pack as ReqChangeUsername;
            ChangeUsername rsp = new ChangeUsername();
            rsp.Userid = req.Userid;
            rsp.Username = req.Username;
            client.Send(rsp);
            UserSystem.me.ChangeUsername(req.Userid, req.Username);
            return;
        }
    }
}
