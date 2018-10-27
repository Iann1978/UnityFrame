package responsors

import (
	"fmt"
	//"net"
	"github.com/golang/protobuf/proto"
	. "../frame"
	. "../packs"
)

func OnPacket_ReqUserAddCoin(client *Client, pack proto.Message)() {
	//conn := client.Conn

	req := pack.(*ReqUserAddCoin)
	fmt.Println("OnPacket_ReqUserAddCoin")
	//fmt.Println(req.Username)

	rsp := &UserAddCoin{}

	userInfo, err := GetUserSystem().GetById(req.Userid);
	CheckError(err)
	rsp.UserInfo = userInfo;
	client.Send(rsp);
}