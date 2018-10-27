package frame
import (
	"fmt"
    "sync"
    "github.com/golang/protobuf/proto"
	//"net"
	"reflect"
)

var m *PackManager
var lock *sync.Mutex = &sync.Mutex {}

func GetPackManager() *PackManager {
    lock.Lock()
    defer lock.Unlock()
    if m == nil {
        m = &PackManager {}
    }
    return m
}

type PackGenerator func()(proto.Message)
type PackResponsor func(*Client, proto.Message)()

type PackManager struct {
	packGenerators map[int32]PackGenerator
	packResponsors map[int32]PackResponsor
	typeToIdMapping map[reflect.Type] int32
}

func (this *PackManager) Init() () {
	this.packGenerators = make(map[int32]PackGenerator)
	this.packResponsors = make(map[int32]PackResponsor)
	this.typeToIdMapping = make(map[reflect.Type] int32)
}

func (this *PackManager) RegisterType(id int32, packType reflect.Type) {
	this.typeToIdMapping[packType] = id;
}

func (this *PackManager) RegistPackGenerator(id int32, generator PackGenerator) {
	this.packGenerators[id] = generator;
	
}

func (this *PackManager) RegistPackGeneratorAndResponsor(	id int32,
															generator PackGenerator, 
															responser PackResponsor) {
	this.packGenerators[id] = generator;
	this.packResponsors[id] = responser;
}

func (this *PackManager) GenPack(packid int32) (proto.Message, error) {
	return this.packGenerators[packid](), nil
}

func (this *PackManager) GetResponsor(packid int32) (PackResponsor, error) {
	return this.packResponsors[packid], nil
}

func (this *PackManager) MappingPackToId(pack interface{}) int32 {
	fmt.Println("MappingPackToId")
	packType := reflect.TypeOf(pack)
	id, ok := this.typeToIdMapping[packType]
	if !ok {
		fmt.Println("Pack ", packType , "not regist")
	}
	fmt.Println(id)
	return id
}

