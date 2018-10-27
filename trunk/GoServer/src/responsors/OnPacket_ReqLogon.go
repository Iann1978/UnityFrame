package responsors

import (
	"fmt"
	. "../packs"
	//"net"
	"github.com/golang/protobuf/proto"
	. "../frame"
)

// Send
// func Send(conn net.Conn, msg proto.Message) () {
// 	buffer, err := proto.Marshal(msg)
// 	if(err != nil) {
// 		fmt.Println(err)
// 		panic("error1")
// 	}


// 	header := make([]byte,5,5);
// 	header[0] = byte(GetPackManager().MappingPackToId(msg))
// 	header[1] = byte(len(buffer))
// 	header[2] = 0
// 	header[3] = 0
// 	header[4] = 0

// 	n, err := conn.Write(header);
// 	if(err != nil || n != 5) {
// 		fmt.Println(err)
// 		panic("error2")
// 	}

// 	n, err = conn.Write(buffer);
// 	if(err != nil || n != len(buffer)) {
// 		fmt.Println(err)
// 		panic("error3")
// 	}
	
// } 

func OnPacket_ReqLogon(client *Client, pack proto.Message)() {
	//conn := client.Conn
	req := pack.(*ReqLogon)
	fmt.Println(req.Username)
	fmt.Println(req.Password)

	rsp := &UserLogon{}
	rsp.ErrorCode  = 0;
	//rsp.UserInfo = &packs.UserInfo{}

	userInfo, err := GetUserSystem().GetByUsername(req.Username)
	CheckError(err)
	rsp.UserInfo = userInfo
	fmt.Println("Id:", userInfo.Id)

	herolist, err := GetHeroSystem().AllHeros(userInfo.Id)
	CheckError(err)
	rsp.UserInfo.HeroList = herolist;
	fmt.Println(rsp.UserInfo.HeroList)
	client.Send(rsp);

}