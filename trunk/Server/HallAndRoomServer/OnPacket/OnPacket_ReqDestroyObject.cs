using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallAndRoomServer;
namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqDestroyObject
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqDestroyObject req = pack as ReqDestroyObject;
            Room room = RoomPool.me.GetByRoomId(client.roomid);

            DestroyObject destroyObject = new DestroyObject();
            destroyObject.RoomId = req.RoomId;
            destroyObject.ObjectId = req.ObjectId;

            room.clientA.Send(destroyObject);
            room.clientB.Send(destroyObject);
        }
    }
}
