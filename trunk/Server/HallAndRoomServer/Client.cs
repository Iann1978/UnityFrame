using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace HallAndRoomServer
{
    class Client
    {
        public static int userIdCounter = 0;
        public enum Status
        {
            Breaked,
            Connectted,
            InHall,
            WaittingForRoom,
            WaittingInRoom,
            Battling,
            Count,
        }

        public bool isLock;
        public int poolid;
        public int userid;
        public int roomid;
        public Socket socket;
        public Status status = Status.Breaked;
        
        public bool IsInRoom()
        {
            return false;
        }

        public void SetSocket(Socket socket)
        {            
            lock (this)
            {                
                this.socket = socket;
                if (socket != null)
                {
                    status = Status.Connectted;
                }
                else
                {
                    status = Status.Breaked;
                }
            }
        }

        public void SetStatus(Status status)
        {
            lock (this)
            {
                this.status = status;
            }
        }

        

        public bool ProcessRecv()
        {
            
            if (socket == null)
                return false;


            byte[] bytes = new byte[5];

            if (socket.Available >= 5)
            {
                int recvlen = socket.Receive(bytes, 0, 5, SocketFlags.Peek);
                {
                    int packlen = (int)(bytes[1]) + ((int)(bytes[2]) << 8) + ((int)(bytes[3]) << 16) + ((int)(bytes[4]) << 24);
                    if (socket.Available >= packlen + 1)
                    {
                        socket.Receive((byte[])bytes, 0, 1, SocketFlags.None);
                        NetworkStream stream = new NetworkStream(socket);
                        object pack = PacketManager.me.readFunSets[(PacketId)bytes[0]](stream);
                        PacketManager.me.onPacketSets[(PacketId)bytes[0]](this, pack);
                        return true;
                    }
                }
            }
            return false;
        }

        public void ProcessSend()
        {

        }

        //ShowHideSmallBalls smallBalls = new ShowHideSmallBalls();
        //BallPosition ball = new BallPosition();
        //public void ProcessSend()
        //{
        //    if (socket == null)
        //        return;



        //    for (int i=0; i<ClientPool.BallCount; i++)
        //    {

        //        ball.BallIndex = i;
        //        ball.X = ClientPool.xx[i];
        //        ball.Y = ClientPool.yy[i];
        //        Send(ball);
        //    }


        //    smallBalls.SmallBallCount = SmallBalls.smallBallCount;
        //    smallBalls.ShowMask = new byte[SmallBalls.byteCount];
        //    smallBalls.BallX = new float[smallBalls.SmallBallCount];
        //    smallBalls.BallY = new float[smallBalls.SmallBallCount];
        //    for (int i = 0; i < SmallBalls.byteCount; i++)
        //    {
        //        smallBalls.ShowMask[i] = SmallBalls.me.balls[i];
        //    }

        //    for (int i = 0; i < smallBalls.SmallBallCount; i++)
        //    {
        //        smallBalls.BallX[i] = SmallBalls.me.ballx[i];
        //        smallBalls.BallY[i] = SmallBalls.me.bally[i];
        //    }

        //    Send(smallBalls);


    //}

        public void Send<T>(T pack)
        {
            if (socket == null)
                return;

            PacketManager.me.Send<T>(socket, pack);
        }
    }
}
