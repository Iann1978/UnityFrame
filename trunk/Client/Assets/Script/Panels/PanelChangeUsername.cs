using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using UnityEngine.UI;
public class PanelChangeUsername : PanelBase
{
    public InputField InputFieldUsername;

    // Use this for initialization
    void Start () {
        InputFieldUsername = controls.GetInputField("InputFieldUsername");
    }

    public override void Refresh()
    {
        InputFieldUsername.text = Gamedata.me.userinfo.Username;
    }

    public override void Init()
    {
        InputFieldUsername = controls.GetInputField("InputFieldUsername");
        InputFieldUsername.text = Gamedata.me.userinfo.Username;
        base.Init();
    }
    //public override void OnShow()
    //{
    //    Debug.Log(InputFieldUsername);
        
    //}

    public void OnButtonConfirmClick()
    {
        InputFieldUsername = controls.GetInputField("InputFieldUsername");
        ReqChangeUsername reqChangeUsername = new ReqChangeUsername();
        reqChangeUsername.Userid = Gamedata.me.userinfo.Id;
        reqChangeUsername.Username = InputFieldUsername.text;
        Net.me.Send(reqChangeUsername);
        Hide();
    }

    public void OnButtonCancelClick()
    {
        Hide();
    }
}
