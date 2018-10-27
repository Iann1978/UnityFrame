using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace HallAndRoomServer
{
    
    
    // RolePool 必须是线程安全的
    class ClientPool : Singleton<ClientPool>
    {
        public const int BallCount = 10;
        public static float[] xx = new float[BallCount];
        public static float[] yy = new float[BallCount];
        static readonly object lockobj = new object();

        Client[] roles;
        public int tail_;
        public int cap_;
        public void Reset(int cap)
        {
            roles = new Client[cap];
            for(int i=0; i < cap; i++)
            {
                Client r = new Client();
                roles[i] = r;
                r.poolid = i;
            }
            tail_ = 0;
            cap_ = cap;
        }

        public Client NewRole()
        {
            lock(lockobj)
            {
                if (tail_ < cap_)
                {
                    Client r = roles[tail_];
                    r.poolid = tail_++;
                    return r;
                }
                return null;
            }
        }

        public List<Client> GetRoles()
        {
            lock (lockobj)
            {
                if (tail_ != 0)
                {
                    List<Client> tmpRoles = new List<Client>();
                    for (int i=0; i<tail_; i++)
                    {
                        tmpRoles.Add(roles[i]);
                    }
                    return tmpRoles;
                }
            }
            return null;
        }

        public void DeleteRole(Client r)
        {
            lock (lockobj)
            {
                int poolid = r.poolid;
                if (poolid == --tail_) return;
                roles[poolid] = roles[tail_];
                roles[poolid].poolid = poolid;
                roles[tail_] = r;
            }
        }


    }
}
