
package frame

import (
    "log"
    "fmt"
    "os"
    "bytes"
    "encoding/binary"
)

func Log(v ...interface{}) {  
    log.Println(v...)  
}  
  
func CheckError(err error) {  
    if err != nil {  
        fmt.Fprintf(os.Stderr, "Fatal error: %s", err.Error())  
        os.Exit(1)  
    }  
}

//整形转换成字节
func IntToBytes(n int) []byte {
    x := int32(n)
 
    bytesBuffer := bytes.NewBuffer([]byte{})
    binary.Write(bytesBuffer, binary.BigEndian, x)
    return bytesBuffer.Bytes()
}
 
//字节转换成整形
func BytesToInt(b []byte) int {
    bytesBuffer := bytes.NewBuffer(b)
 
    var x int32
    binary.Read(bytesBuffer, binary.BigEndian, &x)
 
    return int(x)
}
