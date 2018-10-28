using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using Assets.Script.OnPacket;

using Assets.Script;
public enum PanelId
{
    PanelLoading = 0,
    PanelMain = 1,
    PanelLogon = 2,
    PanelSetting = 3,
    PanelHeroList = 4,
    PanelTeamInfo = 5,
    PanelChangeUsername = 6,
    PanelChangeIconFrame = 7,
    PanelChoiceImageFrame = 8,
    PanelGetMoney = 9,
    PanelGetMoneyResult = 10,
    PanelGetVIT = 11,
    Count,
}



public class Game : SingletonMonoBehaviourNoCreate<Game>{
    
    public PanelBase panelLoadingPrefab;



    void InitGamedata()
    {
        Gamedata.me.userinfo = new UserInfo();
        Gamedata.me.userinfo.Id = 0;
        Gamedata.me.userinfo.TeamLevel = 16;
        Gamedata.me.userinfo.Username = "InitUsername";
    }

    void InitPanelStep0()
    {
        PanelManager.me.InstanceUiBase = delegate (PanelBase prefab) { PanelBase panelBase = Instantiate<PanelBase>(prefab); return panelBase; };
        
        PanelManager.me.RegistPrefab((int)PanelId.PanelLoading, panelLoadingPrefab);
        PanelManager.me.RegistPrefab((int)PanelId.PanelLogon, AssetManager.me.GetPanel("PanelLogon"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelMain, AssetManager.me.GetPanel("PanelMain"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelSetting, AssetManager.me.GetPanel("PanelSetting"));

        PanelManager.me.RegistPrefab((int)PanelId.PanelHeroList, AssetManager.me.GetPanel("PanelHeroList"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelTeamInfo, AssetManager.me.GetPanel("PanelTeamInfo"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelChangeUsername, AssetManager.me.GetPanel("PanelChangeUsername"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelChangeIconFrame, AssetManager.me.GetPanel("PanelChangeIconFrame"));

        PanelManager.me.RegistPrefab((int)PanelId.PanelGetMoney, AssetManager.me.GetPanel("PanelGetMoney"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelGetMoneyResult, AssetManager.me.GetPanel("PanelGetMoneyResult"));
        PanelManager.me.RegistPrefab((int)PanelId.PanelGetVIT, AssetManager.me.GetPanel("PanelGetVIT"));
        
        //PanelManager.me.Init();
        PanelManager.me.HideAll();
        PanelManager.me.Get((int)PanelId.PanelLoading).Show();
    }

    //void InitPanelStep1()
    //{
    //    Debug.Log("InitPanelStep1");
    //    for (int id = (int)PanelId.PanelLoading+1; id<(int)PanelId.Count; id++)
    //    {
    //        Debug.Log((PanelId)id);
    //        PanelBase panel = AssetManager.me.Get<PanelBase>(AssetClass.Panel, id);
    //        PanelManager.me.RegistPrefab(id, panel);
    //    }
    //}

    void RegistPackets()
    {
        Net.me.RegistPacketAndResponFunc<ReqLogon>((int)PacketId.ReqLogon);
        Net.me.RegistPacketAndResponFunc<UserLogon>((int)PacketId.UserLogon, OnPacket_UserLogon.OnPacket);

        Net.me.RegistPacketAndResponFunc<ReqRegist>((int)PacketId.ReqRegist);
        Net.me.RegistPacketAndResponFunc<UserRegist>((int)PacketId.UserRegist, OnPacket_UserRegist.OnPacket);

        Net.me.RegistPacketAndResponFunc<ReqChangeTeamIconFrame>((int)PacketId.ReqChangeTeamIconFrame);
        Net.me.RegistPacketAndResponFunc<TeamIconFrameChanged>((int)PacketId.TeamIconFrameChanged, OnPacket_TeamIconFrameChanged.OnPacket);

        //UserAddCoin
        Net.me.RegistPacketAndResponFunc<ReqUserAddCoin>((int)PacketId.ReqUserAddCoin);
        Net.me.RegistPacketAndResponFunc<UserAddCoin>((int)PacketId.UserAddCoin, OnPacket_UserAddCoin.OnPacket);

        // ChangeUsername
        Net.me.RegistPacketAndResponFunc<ReqChangeUsername>((int)PacketId.ReqChangeUsername);
        Net.me.RegistPacketAndResponFunc<ChangeUsername>((int)PacketId.ChangeUsername, OnPacket_ChangeUsername.OnPacket);
    }

    IEnumerator InitAssetManager()
    {
        AssetConfig.me.Load();
        BundleAssetConfig.me.Load();
        yield return AssetManager.me.Load();        
       // StartCoroutine(IEnumerator)
    }

    // Use this for initialization
    IEnumerator Start ()
    {

        // Open Local Database
        Database.me.Open();
        Database.me.Regist<HeroTableData>();
        //Database.me.GetById<HeroTableData>(12);

        yield return InitAssetManager();

        InitGamedata();

        // 初始化基础Ui
        // 显示splash or loading


        InitPanelStep0();

        RegistPackets();

        Net.me.Connect(NetConfig.me.current.a, NetConfig.me.current.b);

        LuaSystem.me.Init();
#if USE_LOG && LOG_FRAME_LUA
        LuaInterface.Debugger.useLog = true;
#else
        LuaInterface.Debugger.useLog = false;
#endif

        // 加载配置表
        //AssetManager.me.Init();


        //// 启动加载        
        //AssetManager.me.DownloadAll(delegate ()
        //{
        // 初始化全部Ui
        //    InitPanelStep1();

        //显示登陆界面
        PanelManager.me.HideAll();
            PanelManager.me.Get((int)PanelId.PanelLogon).Show();
        //});

        

    }

    void OnDisable()
    {
        Net.me.DisConncet();
        Database.me.Close();
    }

    void Update()
    {
        Net.me.TickFunc();
    }


}
