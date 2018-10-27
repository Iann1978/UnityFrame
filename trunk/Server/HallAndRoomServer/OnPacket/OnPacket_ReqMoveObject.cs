using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqMoveObject
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqMoveObject req = pack as ReqMoveObject;

            Room room = RoomPool.me.GetByRoomId(client.roomid);
            MoveObject moveObject = new MoveObject();
            moveObject.ObjectId = req.ObjectId;
            moveObject.RoomId = req.RoomId;
            moveObject.X = req.X;
            moveObject.Y = req.Y;

            room.clientA.Send(moveObject);
            room.clientB.Send(moveObject);
        }
    }
}
