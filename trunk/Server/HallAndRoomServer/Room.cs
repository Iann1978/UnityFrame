using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallAndRoomServer
{


    class Room : IPoolItem
    {
        // implement for IPoolItem
        int _poolid;
        public int poolid { get { return _poolid; } set { _poolid = poolid; } }


        static int roomIdCounter = 0;
        public int objIdCounter = 0;
        int _roomId;
        public int roomId { get { return _roomId; } }
        public void Reset()
        {
            _roomId = ++roomIdCounter;
            objIdCounter = 0;
        }

        public Client GetOther(Client one)
        {
            if (clientA == one)
            {
                return clientB;
            }

            if (clientB == one)
            {
                return clientA;
            }
            return null;
        }

        public Client clientA;
        public Client clientB;
    }

    class RoomPool : SingtonPool<Room, RoomPool>
    {
        Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        Room readyRoom = null;
        public Room GetOneReadyRoom()
        {
            lock(this)
            {
                if (readyRoom != null)
                {
                    Room retRoom = readyRoom;
                    readyRoom = null;
                    return retRoom;
                }
                else
                {
                    readyRoom = New();
                    readyRoom.Reset();
                    rooms.Add(readyRoom.roomId, readyRoom);
                    return readyRoom;
                }
            }   
        }

        public void FreeRoom(Room room)
        {
            rooms.Remove(room.roomId);
            Delete(room);
        }

        public Room GetByRoomId(int roomId)
        {
            if (rooms.ContainsKey(roomId))
                return rooms[roomId];
            return null;
        }
      
    }



    

}
