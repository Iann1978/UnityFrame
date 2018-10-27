using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerDemo
{
    class OnPacketPerson
    {
        public static void OnPacket(Client client, Person person)
        {
            PacketId packid = PacketManager.me.typeIdSets[typeof(Person)];
        }
    }
}
