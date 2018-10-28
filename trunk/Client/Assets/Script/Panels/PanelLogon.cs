using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using System.Reflection;
using System;
using UnityEngine.UI;

public class PanelLogon : PanelBase{

    InputField username;
    InputField password;
    public override void Init()
    {
        username = controls.GetInputField("InputFieldUsername");
        password = controls.GetInputField("InputFieldPassword");
        base.Init();
        
    }

    public void Awake()
    {
        Debug.Log("PanelLogon.Awake");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnButtonLogonClick()
    {
        Debug.Log("OnButtonLogonClick");
        //Hide();
        //PanelManager.me.Get((int)PanelId.PanelMain).Show();
        username = controls.GetInputField("InputFieldUsername");
        password = controls.GetInputField("InputFieldPassword");
        ReqLogon reqLogon = new ReqLogon();
        reqLogon.Username = username.text;
        reqLogon.Password = password.text;
        //Net.me.Send(reqLogon);
        Net.me.Send2(reqLogon);
    }

    public void OnButtonRegistClick()
    {
        Debug.Log("OnButtonRegistClick");
       // PanelManager.me.Get((int)PanelId.PanelGetVIT).Show();
       //// PanelManager.me.Get((int)PanelId.PanelTeamInfo).Show();
       //// PanelManager.me.Get((int)PanelId.PanelLogon).Hide();
       // return;

        InputField username = controls.GetInputField("InputFieldUsername");
        InputField password = controls.GetInputField("InputFieldPassword");
        
        ReqRegist reqRegist = new ReqRegist();
        reqRegist.Username = username.text;
        reqRegist.Password = password.text;
        Net.me.Send(reqRegist);
    }
}
