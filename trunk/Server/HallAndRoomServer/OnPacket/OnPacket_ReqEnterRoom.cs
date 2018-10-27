using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqEnterRoom
    {
        
        public static void OnPacket(Client client, object pack)
        {
            Room room = RoomPool.me.GetOneReadyRoom();
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.RoomId = room.roomId;
            client.Send(roomInfo);

            if (room.clientA == null)
            {
                room.clientA = client;
                client.roomid = room.roomId;
            }
            else
            {
                room.clientB = client;
                client.roomid = room.roomId;


                //RoomInfo roomInfo = new RoomInfo();
                //roomInfo.RoomId = room.roomId;
                //client.Send(roomInfo);

                //{
                //    UserEnterRoom userEnterRoom = new UserEnterRoom();
                //    userEnterRoom.UserId = room.clientA.userid;
                //    userEnterRoom.RoomId = room.roomId;
                //    client.Send(userEnterRoom);
                //}

                //{
                //    UserEnterRoom userEnterRoom = new UserEnterRoom();
                //    userEnterRoom.UserId = room.clientB.userid;
                //    userEnterRoom.RoomId = room.roomId;
                //    client.Send(userEnterRoom);
                //}

                StartBattle startBattle = new StartBattle();
                startBattle.RoomId = room.roomId;
                room.clientA.Send(startBattle);
                room.clientB.Send(startBattle);

                CreateObject createObjectA = new CreateObject();
                createObjectA.UserId = room.clientA.userid;
                createObjectA.RoomId = room.roomId;
                createObjectA.ObjectId = ++room.objIdCounter;
                createObjectA.ObjectType = 1;
                createObjectA.X = 0;
                createObjectA.Y = 0;
                

                CreateObject createObjectB = new CreateObject();
                createObjectB.UserId = room.clientB.userid;
                createObjectB.RoomId = room.roomId;
                createObjectB.ObjectId = ++room.objIdCounter;
                createObjectB.ObjectType = 1;
                createObjectB.X = 0;
                createObjectB.Y = 0;

                room.clientA.Send(createObjectA);
                room.clientA.Send(createObjectB);

                room.clientB.Send(createObjectA);
                room.clientB.Send(createObjectB);
            }
            

        }
    }
}
