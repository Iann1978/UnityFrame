package frame

import(
	"sync"
	//"fmt"
)

var g_ClientPool *ClientPool
var lock_ClientPool *sync.Mutex = &sync.Mutex {}

func GetClientPool() *ClientPool {
    lock_ClientPool.Lock()
    defer lock_ClientPool.Unlock()
    if g_ClientPool == nil {
        g_ClientPool = &ClientPool {}
    }
    return g_ClientPool
}

type ClientPool struct {
	Pool
}

func (this *ClientPool) ResetClientPool(cap_ int32) {
	this.Reset(5, func()(IPoolItem){return &Client{}})
}

func (this *ClientPool) NewClient() (*Client) {
	return this.New().(*Client)
}

func (this *ClientPool) DeleteClient(client *Client) {
	this.Delete(client)
}


