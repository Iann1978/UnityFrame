using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using ProtoBuf;
using System.Net;
using System.Threading;

namespace HallAndRoomServer
{
    class ListenThread
    {

        private Socket server;
        public ListenThread()
        {
            string ip = "127.0.0.1";
            //string ip = "192.168.1.153";
            //string ip = "10.141.0.222";
            Console.WriteLine("Listen:" + ip);
            //初始化IP地址
            //IPAddress local = IPAddress.Parse("192.168.1.153");
            //IPAddress local = IPAddress.Parse("123.206.17.72");
            IPAddress local = IPAddress.Parse(ip);
            //IPAddress local = IPAddress.Parse("10.141.0.222");
            IPEndPoint iep = new IPEndPoint(local, 13000);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //将套接字与本地终结点绑定
            server.Bind(iep);
            //在本地13000端口号上进行监听
            server.Listen(20);
            Console.WriteLine("等待客户机进行连接......");
            while (true)
            {
                //得到包含客户端信息的套接字
                Socket socket = server.Accept();
                Client r = ClientPool.me.NewRole();
                r.SetSocket(socket);
                //r.socket = client;
                Console.WriteLine("One Client Connect.");
            }
        }
    }
}
