using HallAndRoomServer.OnPacket;

namespace HallAndRoomServer
{
    internal class Boot
    {
        private static void Main(string[] args)
        {
            Database.me.Connect();

            RegistPackets();
            ClientPool.me.Reset(1000);
            RoomPool.me.Reset(20);
            new WorkThread();
            new ListenThread();
        }

        // 注册网络消息及消息回调函数。
        public static void RegistPackets()
        {
            PacketManager.me.RegistPacketAndResponFunc<ReqRegist>(PacketId.ReqRegist, OnPacket_ReqRegist.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<UserRegist>(PacketId.UserRegist);

            PacketManager.me.RegistPacketAndResponFunc<ReqLogon>(PacketId.ReqLogon, OnPacket_ReqLogon.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<UserLogon>(PacketId.UserLogon);
            PacketManager.me.RegistPacketAndResponFunc<ReqLogout>(PacketId.ReqLogout, OnPacket_ReqLogout.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<UserLogout>(PacketId.UserLogout);
            //ReqUserAddCoin
            PacketManager.me.RegistPacketAndResponFunc<ReqUserAddCoin>(PacketId.ReqUserAddCoin,
                OnPacket_ReqUserAddCoin.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<UserAddCoin>(PacketId.UserAddCoin);
            // ReqChangeTeamIconFrame
            PacketManager.me.RegistPacketAndResponFunc<ReqChangeTeamIconFrame>(PacketId.ReqChangeTeamIconFrame,
                OnPacket_ReqChangeTeamIconFrame.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<TeamIconFrameChanged>(PacketId.TeamIconFrameChanged);

            // ReqChangeUsername
            PacketManager.me.RegistPacketAndResponFunc<ReqChangeUsername>(PacketId.ReqChangeUsername,
                OnPacket_ReqChangeUsername.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<ChangeUsername>(PacketId.ChangeUsername);

            PacketManager.me.RegistPacketAndResponFunc<ReqEnterRoom>(PacketId.ReqEnterRoom,
                OnPacket_ReqEnterRoom.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<UserEnterRoom>(PacketId.UserEnterRoom);
            PacketManager.me.RegistPacketAndResponFunc<ReqLeaveRoom>(PacketId.ReqLeaveRoom,
                OnPacket_ReqLeaveRoom.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<UserLeaveRoom>(PacketId.UserLeaveRoom);
            PacketManager.me.RegistPacketAndResponFunc<RoomInfo>(PacketId.RoomInfo);
            PacketManager.me.RegistPacketAndResponFunc<StartBattle>(PacketId.StartBattle);
            PacketManager.me.RegistPacketAndResponFunc<EndBattle>(PacketId.EndBattle);

            PacketManager.me.RegistPacketAndResponFunc<ReqCreateObject>(PacketId.ReqCreateObject,
                OnPacket_ReqCreateObject.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<CreateObject>(PacketId.CreateObject);
            PacketManager.me.RegistPacketAndResponFunc<ReqDestroyObject>(PacketId.ReqDestroyObject,
                OnPacket_ReqDestroyObject.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<DestroyObject>(PacketId.DestroyObject);

            PacketManager.me.RegistPacketAndResponFunc<ReqMoveObject>(PacketId.ReqMoveObject,
                OnPacket_ReqMoveObject.OnPacket);
            PacketManager.me.RegistPacketAndResponFunc<MoveObject>(PacketId.MoveObject);
        }
    }
}