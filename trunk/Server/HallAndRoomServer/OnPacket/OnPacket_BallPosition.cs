using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerDemo
{
    class OnPacket_BallPosition
    {
        public static void OnPacket(Client client, BallPosition ballPosition)
        {
            PacketId packid = PacketManager.me.typeIdSets[typeof(BallPosition)];
            ClientPool.xx[ballPosition.BallIndex] = ballPosition.X;
            Console.WriteLine("BallId:" + ballPosition.BallIndex + " X:" + ballPosition.X);
        }
    }

    class OnPacket_BallDeitaPosition
    {
        public static void OnPacket(Client client, BallDeitaPosition ballPosition)
        {
            PacketId packid = PacketManager.me.typeIdSets[typeof(BallPosition)];
            ClientPool.xx[ballPosition.BallIndex] += ballPosition.DeitaX;
            ClientPool.yy[ballPosition.BallIndex] += ballPosition.DeitaY;

            float x = ClientPool.xx[ballPosition.BallIndex];
            float y = ClientPool.yy[ballPosition.BallIndex];

            for (int i = 0; i < SmallBalls.smallBallCount; i++)
            {
                float sx = SmallBalls.me.ballx[i];
                float sy = SmallBalls.me.bally[i];
                float dx = x - sx;
                float dy = y - sy;
                float len2 = dx * dx + dy * dy;
                if (len2 < 0.25f)
                {
                    SmallBalls.me.HideBall(i);
                }
            }

        }
    }
}
