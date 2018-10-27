using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.IO;
using System.Net;
using System.Net.Sockets;


public enum PacketId
{
    ReqLogon,
    UserLogon,
    ReqLogout,
    UserLogout,
    ReqEnterRoom,
    UserEnterRoom,
    ReqLeaveRoom,
    UserLeaveRoom,
    RoomInfo,    
    StartBattle,
    EndBattle,
    ReqCreateObject,
    CreateObject,
    ReqDestroyObject,
    DestroyObject,
    ReqMoveObject,
    MoveObject,
    Count,
}



[ProtoContract]
public class ReqLogon
{
    [ProtoMember(1)]
    public int UserId { get; set; }
}


[ProtoContract]
public class UserLogon
{
    [ProtoMember(1)]
    public int UserId { get; set; }
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
    public int ObjectType { get; set; }// 1 manual control, 2 computer control

    [ProtoMember(4)]
    public float X { get; set; }

    [ProtoMember(5)]
    public float Y { get; set; }
}

[ProtoContract]
public class CreateObject
{
    [ProtoMember(1)]
    public int UserId{ get; set; }

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




