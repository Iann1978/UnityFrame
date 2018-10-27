using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqChangeTeamIconFrame
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqChangeTeamIconFrame req = pack as ReqChangeTeamIconFrame;
            TeamIconFrameChanged rsp = new TeamIconFrameChanged();

            UserSystem.me.ChangeTeamIconFrame(req.Userid, req.TeamIconFrame);
            rsp.Userid = req.Userid;
            rsp.TeamIconFrame = req.TeamIconFrame;
            client.Send(rsp);            
        }
    }
}
