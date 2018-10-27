package responsors

import (
	"fmt"
	//"net"
	"github.com/golang/protobuf/proto"
	. "../frame"
	. "../packs"
)

func OnPacket_ReqChangeTeamIconFrame(client *Client, pack proto.Message)() {
	//conn := client.Conn
	req := pack.(*ReqChangeTeamIconFrame)
	fmt.Println("OnPacket_ReqChangeTeamiconFrame")

	rsp := &TeamIconFrameChanged{req.Userid, req.TeamIconFrame}

	err := GetUserSystem().ChangeTeamIconFrame(req.Userid, req.TeamIconFrame);
	CheckError(err)

	
	client.Send(rsp);
}