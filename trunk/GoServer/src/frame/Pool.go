package frame

type IPoolItem interface {
	GetPoolIndex() (int32)
	SetPoolIndex(int32)
}

type PoolItemGenerator func()(IPoolItem)

type Pool struct {
	datas []IPoolItem
	tail_ int32
	cap_ int32
}

func (this *Pool) Reset(cap_ int32, generator PoolItemGenerator) {
	this.datas = make([]IPoolItem, cap_, cap_)
	for i := int32(0); i < cap_; i++ {
		item := generator()
		item.SetPoolIndex(i)
		this.datas[i] = item;
       // sum += i
    }
    this.tail_ = 0;
    this.cap_ = cap_
}

func (this *Pool) New() (IPoolItem) {
	if this.tail_ < this.cap_ {
		item := this.datas[this.tail_]
		this.tail_++
		item.SetPoolIndex(this.tail_)
		return item
	}
	return nil
} 

func (this *Pool) All() ([]IPoolItem) {
	items := make([]IPoolItem,this.tail_,this.tail_)
	for i := int32(0); i < this.tail_; i++ {
		items[i] = this.datas[i];
	}
	return items;
}

func (this *Pool) Delete(item IPoolItem) {
	index := item.GetPoolIndex()
	this.tail_--
	if index == this.tail_ {
		return
	}
	this.datas[index] = this.datas[this.tail_]
	this.datas[index].SetPoolIndex(index)
	this.datas[this.tail_] = item
	item.SetPoolIndex(this.tail_)

}

