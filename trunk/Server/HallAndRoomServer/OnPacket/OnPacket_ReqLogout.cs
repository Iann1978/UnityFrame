using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqLogout
    {
        public static void OnPacket(Client client, object pack)
        {
            client.SetStatus(Client.Status.Connectted);

        }
    }
}
