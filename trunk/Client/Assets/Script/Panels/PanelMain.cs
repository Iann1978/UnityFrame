using Assets.Script.Frame;
using UnityEngine;
using UnityEngine.UI;

public class PanelMain : PanelBase
{
    public override void Init()
    {
        base.Init();
    }

    public void OnButtonAddCoinClick()
    {
        //Hide();
        PanelManager.me.Get((int) PanelId.PanelGetMoney).Show();
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnButtonSettingClick()
    {
        Debug.Log("OnButtonSettingClick");
        //Hide();
        PanelManager.me.Get((int) PanelId.PanelSetting).Show();
    }
}