using System;
using System.Text;
using ProtoBuf;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Assets.Script.Frame;

class Packet
{
    public Packet(int packeid, object obj)
    {
        this.packeid = packeid;
        this.obj = obj;
    }
    public int packeid;
    public object obj;
}
delegate object TryRead(Stream stream);
delegate void TryWrite(Stream stream, object pack);
public delegate void OnPacket(object pack);
    
public class Net : Singleton<Net>
{
    bool connectted = false;
    Socket client;
    Queue<Packet> recvQueue;
    Thread recvThread;
    Dictionary<int, TryRead> readFunSets = new Dictionary<int, TryRead>();
    Dictionary<int, TryWrite> writeFunSets = new Dictionary<int, TryWrite>();
    Dictionary<Type, int> typeIdSets = new Dictionary<Type, int>();
    Dictionary<int, OnPacket> onPacketSets = new Dictionary<int, OnPacket>();

    static object TryRead<T>(Stream stream)
    {
        T obj = Serializer.DeserializeWithLengthPrefix<T>(stream, PrefixStyle.Fixed32);
        return obj;
    }

    static void TryWrite<T>(Stream stream, object pack) where T: class
    {
        Serializer.SerializeWithLengthPrefix<T>(stream, pack as T, PrefixStyle.Fixed32);
    }

    public void RegistPacketAndResponFunc<T>(int packid, OnPacket onPacket = null) where T:class
    {
        readFunSets.Add(packid, TryRead<T>);
        writeFunSets.Add(packid, TryWrite<T>);
        typeIdSets.Add(typeof(T), packid);
        if (onPacket != null)
        {
            onPacketSets.Add(packid, onPacket);
        }
    }

    public void DisConncet()
    {
        connectted = false;
        try
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
       
    }

    public void Connect(string ip, int port, Action cb = null)
    {
        IPAddress local = IPAddress.Parse(ip);
        IPEndPoint iep = new IPEndPoint(local, 13000);
        try
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iep);
        }
        catch (SocketException)
        {
            Console.WriteLine("无法连接到服务器！");
            return;
        }
        finally
        {
            recvQueue = new Queue<Packet>();
        }
        connectted = true;

        if (cb!=null) cb();        
        recvThread = new Thread(new ThreadStart(RecvFunc));
        recvThread.Start();
    }
    byte[] packid = new byte[1];
    byte[] bytes = new byte[1024];
    public void RecvFunc()
    {
        while(connectted)
        {

            int packlen;
            if (client.Available >= 5)
            {
                if ((packlen = client.Receive(packid, 0, 1, SocketFlags.None)) == 1)
                {
                    //NetworkStream stream = new NetworkStream(client);
                    //object pack = readFunSets[(int)packid[0]](stream);
                    //lock (recvQueue)
                    //{
                    //    recvQueue.Enqueue(new Packet((int)packid[0], pack));
                    //}

                    while (client.Available < 4)
                    {
                        Thread.Sleep(1);
                    }

                    client.Receive(bytes, 0, 4, SocketFlags.Peek);
                    packlen = (int)(bytes[0]) + ((int)(bytes[1]) << 8) + ((int)(bytes[2]) << 16) + ((int)(bytes[3]) << 24);


                    while (client.Available < packlen)
                    {
                        Thread.Sleep(1);
                    }

                    NetworkStream stream = new NetworkStream(client);
                    object pack = readFunSets[(int)packid[0]](stream);
                    lock (recvQueue)
                    {
                        recvQueue.Enqueue(new Packet((int)packid[0], pack));
                    }

                }
            }
            else
            {
                Thread.Sleep(5);
            }
        }
           
    }
        

    public void Send<T>(T pack)
    {
        if (connectted)
        {
            NetworkStream stream = new NetworkStream(client);
            byte[] packid = new byte[1] { (byte)typeIdSets[typeof(T)] };
            stream.Write(packid, 0, 1);
            Serializer.SerializeWithLengthPrefix<T>(stream, pack, PrefixStyle.Fixed32);
        }        
    }

    public void Send2(object pack)
    {
        if (connectted)
        {
            NetworkStream stream = new NetworkStream(client);
            byte[] packid = new byte[1] { (byte)typeIdSets[pack.GetType()] };
            stream.Write(packid, 0, 1);
            writeFunSets[packid[0]](stream, pack);
            //Serializer.SerializeWithLengthPrefix(stream, pack , PrefixStyle.Fixed32);
        }
    }

    public void TickFunc()
    {
        Packet tuple = null;
        
        if (recvQueue != null)
        {
            int maxMessagePerCount = 150;
            while (recvQueue.Count > 0 && maxMessagePerCount>0)
            {
                lock (recvQueue)
                {
                    if (recvQueue.Count > 0)
                        tuple = recvQueue.Dequeue();
                }
                if (tuple != null)
                {
                    onPacketSets[tuple.packeid](tuple.obj);
                    --maxMessagePerCount;
                }
                    
            }
            
            
        }
            
    }

    public void Close()
    {
        client.Close();
    }

}
