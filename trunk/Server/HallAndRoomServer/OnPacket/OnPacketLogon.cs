using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerDemo
{
    class OnPacketLogon
    {
        static int ballIndex = 0;
        public static void OnPacket(Client client, Logon logon)
        {
            PacketId packid = PacketManager.me.typeIdSets[typeof(Logon)];
            
            LogonResult result = new LogonResult();         
            result.BallIndex = ballIndex++;
            result.X = ClientPool.xx[result.BallIndex];
            result.Y = ClientPool.yy[result.BallIndex];
            client.Send(result);

            AllSmallBalls smallBalls = new AllSmallBalls();
            smallBalls.SmallBallCount = SmallBalls.smallBallCount;
            smallBalls.BallX = new float[SmallBalls.smallBallCount];
            smallBalls.BallY = new float[smallBalls.SmallBallCount];
            for (int i = 0; i < smallBalls.BallX.Length; i++)
            {
                smallBalls.BallX[i] = SmallBalls.me.ballx[i];
                smallBalls.BallY[i] = SmallBalls.me.bally[i];
            }
            client.Send(smallBalls);
            client.SetStatus(Client.Status.Working);


        }
    }
}
