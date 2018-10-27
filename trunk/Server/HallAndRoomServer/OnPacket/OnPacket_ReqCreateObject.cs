using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallAndRoomServer;
namespace HallAndRoomServer.OnPacket
{
    class OnPacket_ReqCreateObject
    {
        public static void OnPacket(Client client, object pack)
        {
            
            ReqCreateObject req = pack as ReqCreateObject;

            Room room = RoomPool.me.GetByRoomId(client.roomid);
            
            CreateObject createObject = new CreateObject();
            createObject.UserId = req.UserId;
            createObject.RoomId = room.roomId;
            createObject.ObjectType = req.ObjectType;
            createObject.ObjectId = ++room.objIdCounter;
            createObject.X = req.X;
            createObject.Y = req.Y;

            //Client toSend = room.clientA == client ? room.clientB : room.clientA;

            //toSend.Send(createObject);
            room.clientA.Send(createObject);
            room.clientB.Send(createObject);
            Console.WriteLine(string.Format("CreateObject ObjectId:{0}", createObject.ObjectId));




            
        }
    }
}
