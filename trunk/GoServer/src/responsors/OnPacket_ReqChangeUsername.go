package responsors

import (
	"fmt"
	//"net"
	"github.com/golang/protobuf/proto"
	. "../frame"
	. "../packs"
)

func OnPacket_ReqChangeUsername(client *Client, pack proto.Message)() {
	//conn := client.Conn

	req := pack.(*ReqChangeUsername)
	fmt.Println("OnPacket_ReqChangeUsername")
	fmt.Println(req.Username)

	rsp := &ChangeUsername{req.Userid, req.Username}

	err := GetUserSystem().ChangeUsername(req.Userid, req.Username);
	CheckError(err)
	client.Send(rsp);
}