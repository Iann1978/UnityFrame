using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using ProtoBuf;

namespace HallAndRoomServer
{
    class WorkThread
    {
        Thread workThread;
        public WorkThread()
        {
            workThread = new Thread(new ThreadStart(ThreadFunc));
            workThread.Start();
        }
         
        public void ProcessClientBreaked(Client r)
        {
            OnPacket.OnClientBreaked.OnBreaked(r);
            r.socket.Close();
            r.SetSocket(null);
            ClientPool.me.DeleteRole(r);
        }

        void ThreadFunc()
        {
            while (true)
            {
                List<Client> tmpRoles = ClientPool.me.GetRoles();
                if (tmpRoles != null)
                {
                    foreach (Client r in tmpRoles)
                    {
                        if (!r.isLock)
                        {
                            lock (r)
                            {
                                r.isLock = true;
                                // Read
                                try
                                {
                                    if (r.socket.Connected == false)
                                    {
                                        ProcessClientBreaked(r);
                                    }
                                    bool recv = r.ProcessRecv();

                                    if (recv)
                                        r.ProcessSend();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("One Client Break");
                                    Console.WriteLine(e.ToString());
                                    ProcessClientBreaked(r);
                                    //OnPacket.OnClientBreaked.OnBreaked(r);
                                    //r.socket.Close();
                                    //r.SetSocket(null);
                                    //ClientPool.me.DeleteRole(r);
                                }
                                r.isLock = false;
                            }

                        }
                        
                       
                    }
                }

                Thread.Sleep(1);

            }
            Thread.Sleep(10);

        }

    }
}
