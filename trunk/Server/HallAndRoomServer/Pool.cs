using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace HallAndRoomServer
{

    interface IPoolItem
    {
        int poolid { get; set; }
    }

    // RolePool 必须是线程安全的
    class Pool<T> where T : class, IPoolItem, new()
    {
        static readonly object lockobj = new object();

        T[] datas;
        public int tail_;
        public int cap_;
        public void Reset(int cap)
        {
            datas = new T[cap];
            for (int i = 0; i < cap; i++)
            {
                T t = new T();
                datas[i] = t;
                t.poolid = i;
            }
            tail_ = 0;
            cap_ = cap;
        }

        public T New()
        {
            lock (lockobj)
            {
                if (tail_ < cap_)
                {
                    T t = datas[tail_];
                    t.poolid = tail_++;
                    return t;
                }
                return null;
            }
        }

        public List<T> GetAll()
        {
            lock (lockobj)
            {
                if (tail_ != 0)
                {
                    List<T> list = new List<T>();
                    for (int i = 0; i < tail_; i++)
                    {
                        list.Add(datas[i]);
                    }
                    return list;
                }
            }
            return null;
        }

        public void Delete(T t)
        {
            lock (lockobj)
            {
                int poolid = t.poolid;
                if (poolid == --tail_) return;
                datas[poolid] = datas[tail_];
                datas[poolid].poolid = poolid;
                datas[tail_] = t;
            }
        }


    }

    class SingtonPool<T1, T2> :Pool<T1> where T1 : class, IPoolItem, new() where T2 : class, new()
    {
        static T2 this_obj;
        static readonly object lockobj = new object();

        public static T2 me
        {
            get
            {
                if (this_obj == null)
                {
                    lock (lockobj)
                    {
                        if (this_obj == null)
                            this_obj = new T2();
                    }
                }
                return this_obj;
            }
        }
    }

}
