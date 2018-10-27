using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqLeaveRoom
    {
        public static void OnPacket(Client client, object pack)
        {
            ReqLeaveRoom req = pack as ReqLeaveRoom;

            Room room = RoomPool.me.GetByRoomId(client.roomid);

            UserLeaveRoom userLeaveRoomA = new UserLeaveRoom();
            userLeaveRoomA.UserId = room.clientA.userid;
            userLeaveRoomA.RoomId = room.roomId;
            room.clientA.Send(userLeaveRoomA);

            UserLeaveRoom userLeaveRoomB = new UserLeaveRoom();
            userLeaveRoomB.UserId = room.clientB.userid;
            userLeaveRoomB.RoomId = room.roomId;
            room.clientB.Send(userLeaveRoomB);

            room.clientA.roomid = -1;
            room.clientB.roomid = -1;
            room.clientA.SetStatus(Client.Status.InHall);
            room.clientB.SetStatus(Client.Status.InHall);

            RoomPool.me.FreeRoom(room);



        }
    }
}
