using ProtoBuf;
using System.Collections;
using System.Collections.Generic;
public enum PacketId
{
    ReqRegist = 0,  // 请求注册
    UserRegist = 1, // 用户注册
    ReqLogon = 2,
    UserLogon = 3,
    ReqLogout = 4,
    UserLogout = 5,
    ReqEnterRoom = 6,
    UserEnterRoom = 7,
    ReqLeaveRoom = 8,
    UserLeaveRoom = 9,
    RoomInfo = 10,
    StartBattle = 11,
    EndBattle = 12,
    ReqCreateObject = 13,
    CreateObject = 14,
    ReqDestroyObject = 15,
    DestroyObject = 16,
    ReqMoveObject = 17,
    MoveObject = 18,
    ReqChangeTeamIconFrame = 19,
    TeamIconFrameChanged = 20,
    ReqUserAddCoin = 21,
    UserAddCoin = 22,
    ReqChangeUsername = 23,
    ChangeUsername = 24,
    Count
}


[ProtoContract]
public class UserInfo
{
    [ProtoMember(1)]
    public int Id { get; set; } // 用戶Id

    [ProtoMember(2)]
    public string Username { get; set; } // 用戶名

    [ProtoMember(3)]
    public int Coin { get; set; } // 金幣

    [ProtoMember(4)]
    public int Diamond { get; set; } // 鑽石

    [ProtoMember(5)]
    public int Vigour { get; set; } // 體力

    [ProtoMember(6)]
    public int VipLevel { get; set; } // Vip等級

    [ProtoMember(7)]
    public int TeamLevel { get; set; } // 戰隊等級

    [ProtoMember(8)]
    public int TeamExp { get; set; } // 戰隊經驗

    [ProtoMember(9)]
    public int TeamIcon { get; set; } // 戰隊頭像

    [ProtoMember(10)]
    public int TeamIconFrame { get; set; } // 戰隊頭像框

    [ProtoMember(11)]
    public int BattlePower { get; set; } // 戰鬥力

    [ProtoMember(12)]
    public int TeamTimes { get; set; } //抽奖次数

    [ProtoMember(13)]
    public List<HeroInfo> HeroList { get; set; } //抽奖次数
}


[ProtoContract]
public class ReqRegist
{
    [ProtoMember(1)]
    public string Username { get; set; }


    [ProtoMember(2)]
    public string Password { get; set; }
}


[ProtoContract]
public class UserRegist
{
    [ProtoMember(1)]
    public int ErrorCode { get; set; } // 0: NoError, 1: Username has already exist, 

    [ProtoMember(2)]
    public UserInfo UserInfo { get; set; }
}

[ProtoContract]
public class ReqLogon
{
    [ProtoMember(1)]
    public string Username { get; set; }


    [ProtoMember(2)]
    public string Password { get; set; }
}


[ProtoContract]
public class UserLogon
{
    [ProtoMember(1)]
    public int ErrorCode { get; set; } // 0: NoError, 1: Username has already exist, 

    [ProtoMember(2)]
    public UserInfo UserInfo { get; set; }
}

[ProtoContract]
public class ReqLogout
{
    [ProtoMember(1)]
    public int UserId { get; set; }
}


[ProtoContract]
public class UserLogout
{
    [ProtoMember(1)]
    public int UserId { get; set; }
}

[ProtoContract]
public class ReqEnterRoom
{
    [ProtoMember(1)]
    public int UserId { get; set; }
}

[ProtoContract]
public class UserEnterRoom
{
    [ProtoMember(1)]
    public int UserId { get; set; }

    [ProtoMember(2)]
    public int RoomId { get; set; }
}

[ProtoContract]
public class ReqLeaveRoom
{
    [ProtoMember(1)]
    public int UserId { get; set; }
}

[ProtoContract]
public class UserLeaveRoom
{
    [ProtoMember(1)]
    public int UserId { get; set; }

    [ProtoMember(2)]
    public int RoomId { get; set; }
}

[ProtoContract]
public class RoomInfo
{
    [ProtoMember(1)]
    public int RoomId { get; set; }
}

[ProtoContract]
public class StartBattle
{
    [ProtoMember(1)]
    public int RoomId { get; set; }
}

[ProtoContract]
public class EndBattle
{
    [ProtoMember(1)]
    public int RoomId { get; set; }
}


[ProtoContract]
public class ReqCreateObject
{
    [ProtoMember(1)]
    public int UserId { get; set; }

    [ProtoMember(2)]
    public int RoomId { get; set; }

    [ProtoMember(3)]
    public int ObjectType { get; set; } // 1 manual control, 2 computer control

    [ProtoMember(4)]
    public float X { get; set; }

    [ProtoMember(5)]
    public float Y { get; set; }
}

[ProtoContract]
public class CreateObject
{
    [ProtoMember(1)]
    public int UserId { get; set; }

    [ProtoMember(2)]
    public int RoomId { get; set; }

    [ProtoMember(3)]
    public int ObjectType { get; set; }

    [ProtoMember(4)]
    public int ObjectId { get; set; }

    [ProtoMember(5)]
    public float X { get; set; }

    [ProtoMember(6)]
    public float Y { get; set; }
}


[ProtoContract]
public class ReqDestroyObject
{
    [ProtoMember(1)]
    public int RoomId { get; set; }

    [ProtoMember(2)]
    public int ObjectId { get; set; }

    [ProtoMember(3)]
    public int ObjectParam { get; set; }
}

[ProtoContract]
public class DestroyObject
{
    [ProtoMember(1)]
    public int RoomId { get; set; }

    [ProtoMember(2)]
    public int ObjectId { get; set; }
}

[ProtoContract]
public class ReqMoveObject
{
    [ProtoMember(1)]
    public int RoomId { get; set; }

    [ProtoMember(2)]
    public int ObjectId { get; set; }

    [ProtoMember(3)]
    public float X { get; set; }

    [ProtoMember(4)]
    public float Y { get; set; }
}

[ProtoContract]
public class MoveObject
{
    [ProtoMember(1)]
    public int RoomId { get; set; }

    [ProtoMember(2)]
    public int ObjectId { get; set; }

    [ProtoMember(3)]
    public float X { get; set; }

    [ProtoMember(4)]
    public float Y { get; set; }
}

[ProtoContract]
public class ReqChangeTeamIconFrame
{
    [ProtoMember(1)]
    public int Userid { get; set; }


    [ProtoMember(2)]
    public int TeamIconFrame { get; set; }
}

[ProtoContract]
public class TeamIconFrameChanged
{
    [ProtoMember(1)]
    public int Userid { get; set; }


    [ProtoMember(2)]
    public int TeamIconFrame { get; set; }
}

public class ReqUserAddCoin
{
    [ProtoMember(1)] //用户id
    public int Userid { get; set; }
}

public class UserAddCoin

{
    [ProtoMember(1)] //用户id
    public int Userid { get; set; }

    [ProtoMember(2)]
    public UserInfo UserInfo { get; set; }

    [ProtoMember(3)]
    public int Coin { get; set; } // 金幣

    [ProtoMember(4)]
    public int Diamond { get; set; } // 鑽石

    [ProtoMember(6)]
    public int VipLevel { get; set; } // Vip等級
}
[ProtoContract]
public class ReqChangeUsername
{
    [ProtoMember(1)] //用户id
    public int Userid { get; set; }

    [ProtoMember(2)] // 更改后的用户名
    public string Username { get; set; }
}
[ProtoContract]
public class ChangeUsername

{
    [ProtoMember(1)] //用户id
    public int Userid { get; set; }

    [ProtoMember(2)] // 更改后的用户名
    public string Username { get; set; }
}


[ProtoContract]
public class HeroInfo
{
    [ProtoMember(1)]
    public int Id { get; set; } // 英雄Id

    [ProtoMember(2)]
    public int Level { get; set; } // 用戶Id

    [ProtoMember(3)]
    public int Star { get; set; } // 用戶Id

    [ProtoMember(5)]
    public int Quality { get; set; } // 用戶Id
}