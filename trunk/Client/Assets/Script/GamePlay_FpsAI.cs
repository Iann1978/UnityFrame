using UnityEngine;
using System.Collections;
using Assets.Script.Frame;





public class GamePlay_FpsAI : SingletonMonoBehaviourNoCreate<GamePlay_FpsAI>
{
    
    public PanelBase panelLoadingPrefab;


    //void InitAssets()
    //{
    //    // 选择资源加载方式


    //    // 加载资源对应的列表

    //    // 加载所有资源
    //    AssetManager.me.Init();
    //}

    //void InitPanelStep0()
    //{
    //    PanelManager.me.InstanceUiBase = delegate (PanelBase prefab) { PanelBase panelBase = Instantiate<PanelBase>(prefab); return panelBase; };
    //    PanelManager.me.RegistPrefab((int)PanelId.PanelLoading, panelLoadingPrefab);
    //    PanelManager.me.Init();
    //    PanelManager.me.HideAll();
    //    PanelManager.me.Get((int)PanelId.PanelLoading).Show();
    //}

    //void InitPanelStep1()
    //{
    //    for (int id = (int)PanelId.PanelLoading+1; id<(int)PanelId.Count; id++)
    //    {
    //        PanelBase panel = AssetManager.me.Get<PanelBase>(AssetClass.Panel, id);
    //        PanelManager.me.RegistPrefab(id, panel);
    //    }
    //}

	// Use this for initialization
	void Start ()
    {


        //// 初始化基础Ui
        //// 显示splash or loading
        //InitPanelStep0();

        //Net.me.RegistPacketAndResponFunc<ReqLogon>((int)PacketId.ReqLogon);
        //Net.me.RegistPacketAndResponFunc<UserLogon>((int)PacketId.UserLogon, OnPacket_UserLogon.OnPacket);
        //Net.me.Connect(NetConfig.me.current.a, NetConfig.me.current.b);

        //LuaSystem.me.Init();

        //// 加载配置表
        //AssetManager.me.Init();
        

        //// 启动加载        
        //AssetManager.me.DownloadAll(delegate ()
        //{
        //    // 初始化全部Ui
        //    InitPanelStep1();

        //    // 显示登陆界面
        //    PanelManager.me.HideAll();
        //    PanelManager.me.Get((int)PanelId.PanelLogon).Show();
        //});

        

    }

    void OnDisable()
    {
        Net.me.DisConncet();
    }

    void Update()
    {
        Net.me.TickFunc();
    }


}
