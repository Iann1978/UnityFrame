using UnityEngine;
using System.Collections;
using Assets.Script.Frame;

public class NetConfig : SingletonMonoBehaviourNoCreate<NetConfig>
{
    public enum Ip
    {
        local = 0,
        inner,
        outer,
    }
    public Ip ip;
    string[] ips = { "127.0.0.1", "192.168.1.153", "123.206.17.72" };
    int[] ports = { 13000, 13000, 13000 };

    public Pair<string, int> current { get { return new Pair<string, int>(ips[(int)ip], ports[(int)ip]);  } }

}
