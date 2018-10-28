using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using System.Reflection;
using System;
using UnityEngine.UI;

public class PanelPropertyBox : PanelBase
{
    Text txCoin;
    Text txDiamond;
    Text txVigour;

    public void GetControls()
    {
        txCoin = GetText("TextCoin");
        txDiamond = GetText("TextDiamond");
        txVigour = GetText("TextVigour");
    }
    public override void Init()
    {
        Debug.Log("PanelPropertyBox(" + name + ").Init");
        bool active = gameObject.activeSelf;
        gameObject.SetActive(true);
        GetControls();
        gameObject.SetActive(active);
        base.Init();
    }

    //public void Awake()
    //{
    //    Debug.Log("PanelPropertyBox(" + name + ").Awake");
    //    GetControls();
    //}

    //public override void OnShow()
    //{
    //    base.OnShow();
    //    GetControls();
    //    txCoin.text = Gamedata.me.userinfo.Coin.ToString();
    //    txDiamond.text = Gamedata.me.userinfo.Diamond.ToString();
    //    txVigour.text = Gamedata.me.userinfo.Vigour.ToString();

    //}
    public void OnButtonAddCoinClick()
    {
        Debug.Log("OnButtonAddCoinClick");
        PanelManager.me.Get((int)PanelId.PanelGetMoney).Show();
    }
    public void OnButtonAddVigourClick()
    {
        Debug.Log("OnButtonAddVigourClick");
        PanelManager.me.Get((int)PanelId.PanelGetVIT).Show();
    }
    public override void Refresh()
    {
        //GetControls();
        txCoin.text = Gamedata.me.userinfo.Coin.ToString();
        txDiamond.text = Gamedata.me.userinfo.Diamond.ToString();
        txVigour.text = Gamedata.me.userinfo.Vigour.ToString();
        base.Refresh();

    }


}
