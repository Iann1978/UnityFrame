using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer.OnPacket
{
    class OnClientBreaked
    {
        public static void OnBreaked(Client client)
        {
            if (client.IsInRoom())
            {
                Room room = RoomPool.me.GetByRoomId(client.roomid);                
                if (room != null)
                {
                    Client other = room.GetOther(client);
                    other.socket.Close();
                    RoomPool.me.FreeRoom(room);
                }
            }
        }
    }
}
