package responsors

import (
	"fmt"
	//"net"
	"github.com/golang/protobuf/proto"
	. "../frame"
	. "../packs"
)

func OnPacket_ReqRegist(client *Client, pack proto.Message)() {
	//conn := client.Conn

	req := pack.(*ReqRegist)
	fmt.Println("OnPacket_ReqRegist")
	fmt.Println(req.Username)

	rsp := &UserLogon{1,nil}
	userInfo, err := GetUserSystem().GetByUsername(req.Username)
	if userInfo != nil	{
		client.Send(rsp)
		return
	}

	userInfo, err = GetUserSystem().NewUser(req.Username, req.Password);
	CheckError(err)
	GetHeroSystem().NewAndInsertHero(userInfo.Id, 2);
	GetHeroSystem().NewAndInsertHero(userInfo.Id, 3);
	userInfo.HeroList, err = GetHeroSystem().AllHeros(userInfo.Id);
	CheckError(err)
	rsp.ErrorCode = 0;
    rsp.UserInfo = userInfo;
	client.Send(rsp);

	

}