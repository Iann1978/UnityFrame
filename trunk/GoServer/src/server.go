package main

import(
	"fmt"
	"net"
	"reflect"
	"github.com/golang/protobuf/proto"
	. "./packs"
	. "./responsors"
    . "./frame"
)


func main() {



	GetDatabase().Connect();

	GetClientPool().ResetClientPool(5);


	GetPackManager().Init();

	GetPackManager().RegistPackGeneratorAndResponsor(2,
		func()(proto.Message) { return &ReqLogon{} }, 
		OnPacket_ReqLogon)
	GetPackManager().RegisterType(3, reflect.TypeOf((*UserLogon)(nil)))

	GetPackManager().RegistPackGeneratorAndResponsor(0,
		func()(proto.Message) { return new(ReqRegist) }, 
		OnPacket_ReqRegist)
	GetPackManager().RegisterType(1, reflect.TypeOf((*UserRegist)(nil)))

	GetPackManager().RegistPackGeneratorAndResponsor(23,
		func()(proto.Message) { return new(ReqChangeUsername) }, 
		OnPacket_ReqChangeUsername)
	GetPackManager().RegisterType(24, reflect.TypeOf((*ChangeUsername)(nil)))

	GetPackManager().RegistPackGeneratorAndResponsor(21,
		func()(proto.Message) { return new(ReqUserAddCoin) }, 
		OnPacket_ReqUserAddCoin)
	GetPackManager().RegisterType(22, reflect.TypeOf((*UserAddCoin)(nil)))

	GetPackManager().RegistPackGeneratorAndResponsor(19,
		func()(proto.Message) { return new(ReqChangeTeamIconFrame) }, 
		OnPacket_ReqChangeTeamIconFrame)
	GetPackManager().RegisterType(20, reflect.TypeOf((*TeamIconFrameChanged)(nil)))





	netListen, err := net.Listen("tcp", "localhost:13000")
	CheckError(err)
	defer netListen.Close()

	Log("Waiting for clients")
	for {
		conn, err := netListen.Accept()
		CheckError(err)
		Log(conn.RemoteAddr().String(), "tcp connect success")

		client := GetClientPool().NewClient()
		client.SetConn(conn)

		go handleConnection(client)
	}
}

func handleConnection(client *Client) {
	//buffer := make([]byte, 2048)
	conn := client.Conn
	for {

		fmt.Println("1:");
		header, err := ReadPackHeader(conn)
		CheckError(err)
		// fmt.Println("header.length:")
		// fmt.Println(header.Length)


		fmt.Println("2:");
		buffer, err := ReadBuffer(conn, header.Length)
		CheckError(err)
		// fmt.Println("bufferlen:")
		// fmt.Println(len(buffer))
		// for k,v := range buffer {
		// 	fmt.Printf("%d:%d\n",k,v)
		// }

		fmt.Println("3:");
		reqLogon, err := GetPackManager().GenPack(header.packid)
		err = proto.Unmarshal(buffer, reqLogon)
		CheckError(err)

		fmt.Println("4:");
    	responsor, err := GetPackManager().GetResponsor(header.packid)
    	CheckError(err)

    	fmt.Println("5:");
    	responsor(client, reqLogon)
	}
}


type PackHeader struct {
	Length 	int32
	packid 	int32
	index	int32
	reserve int32
}


func ReadPackHeader(conn net.Conn) (header PackHeader, err error) {
	var count int; count = 5
	buffer := make([]byte, count)
	var n int; n = 0;
	var dn int;
	err = nil;
	for n < count && err == nil {
		dn, err = conn.Read(buffer[n:])
		fmt.Println("dn:")
		fmt.Println(dn)
		n = n+dn
	}

	if err != nil {
		fmt.Println("error in ReadPackHeader")
	}

	if err == nil {
		fmt.Println("buffer:")
		fmt.Println(buffer[0])
    	fmt.Println(buffer[1])
    	fmt.Println(buffer[2])
    	fmt.Println(buffer[3])
    	fmt.Println(buffer[4])
    	header.Length = int32(buffer[1]);
    	header.packid = int32(buffer[0]);
	}

	return header, err
}

func ReadBuffer(conn net.Conn, count int32) (buffer []byte, err error) {
	buffer = make([]byte, count)
	var n int; n = 0;
	var dn int;
	err = nil;
	for n < int(count) && err == nil {
		dn, err = conn.Read(buffer[n:])
		n = n + dn
	}
	return buffer, err
}

