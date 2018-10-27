package frame
import(
	"net"
	"fmt"
	"github.com/golang/protobuf/proto"
)

type Client struct {
	Conn net.Conn
	poolIndex int32
}

func (this *Client) GetPoolIndex() (int32) {
	return this.poolIndex;
}

func (this *Client) SetPoolIndex(poolIndex int32) () {
	this.poolIndex = poolIndex
}

func (this *Client) GetConn() (net.Conn) {
	return this.Conn
}

func (this *Client) SetConn(conn net.Conn) () {
	this.Conn = conn
}

func (this *Client) Send( msg proto.Message) () {
	conn := this.Conn
	buffer, err := proto.Marshal(msg)
	if(err != nil) {
		fmt.Println(err)
		panic("error1")
	}


	header := make([]byte,5,5);
	header[0] = byte(GetPackManager().MappingPackToId(msg))
	header[1] = byte(len(buffer))
	header[2] = 0
	header[3] = 0
	header[4] = 0

	n, err := conn.Write(header);
	if(err != nil || n != 5) {
		fmt.Println(err)
		panic("error2")
	}

	n, err = conn.Write(buffer);
	if(err != nil || n != len(buffer)) {
		fmt.Println(err)
		panic("error3")
	}
	
} 
