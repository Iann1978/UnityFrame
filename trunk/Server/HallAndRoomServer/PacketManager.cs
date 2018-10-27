using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProtoBuf;
using System.Net.Sockets;

namespace HallAndRoomServer
{
    delegate object TryRead(Stream stream);
    delegate void TryWrite(Stream stream, object pack);
    delegate void FuncOnPacket(Client client, object pack);
    
    class PacketManager : Singleton<PacketManager>
    {
        public Dictionary<PacketId, TryRead> readFunSets = new Dictionary<PacketId, TryRead>();
        public Dictionary<PacketId, TryWrite> writeFunSets = new Dictionary<PacketId, TryWrite>();
        public Dictionary<Type, PacketId> typeIdSets = new Dictionary<Type, PacketId>();
        public Dictionary<PacketId, FuncOnPacket> onPacketSets = new Dictionary<PacketId, FuncOnPacket>();

        static object TryRead<T>(Stream stream)
        {
            T obj = Serializer.DeserializeWithLengthPrefix<T>(stream, PrefixStyle.Fixed32);
            return obj;
        }

        static void TryWrite<T>(Stream stream, object pack)
        {
            byte[] packid = new byte[1] { (byte)PacketManager.me.typeIdSets[typeof(T)] };
            stream.Write(packid, 0, 1);
            Serializer.SerializeWithLengthPrefix<T>(stream, (T)pack, PrefixStyle.Fixed32);
        }

        public void RegistPacketAndResponFunc<T>(PacketId packid, FuncOnPacket onPacket = null)
        {
            readFunSets.Add(packid, TryRead<T>);
            writeFunSets.Add(packid, TryWrite<T>);
            typeIdSets.Add(typeof(T), packid);
            if (onPacket != null)
            {
                onPacketSets.Add(packid, onPacket);
            }
        }

        public void Send<T>(Socket client, T pack)
        {
            NetworkStream stream = new NetworkStream(client);
            byte[] packid = new byte[1] { (byte)typeIdSets[typeof(T)] };
            stream.Write(packid, 0, 1);
            Serializer.SerializeWithLengthPrefix<T>(stream, pack, PrefixStyle.Fixed32);

        }
    }
}
