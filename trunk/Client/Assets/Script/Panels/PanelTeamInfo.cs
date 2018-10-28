using Assets.Script.Frame;
using UnityEngine;
using UnityEngine.UI;

public class PanelTeamInfo : PanelBase
{
    public Image HeroImageBox;
    public Text LevelLimitText;
    public GameObject PanelChangeIconFrame;
    public Text TeamExpText;
    public Text TeamLevelText;
    public Text UserIDText;
    public Text TextUsername;

    public override void Init()
    {
        TeamLevelText = GetText("TextTeamLevel");
        LevelLimitText = GetText("TextLevelLimit");
        UserIDText = GetText("TextUserID");
        TeamExpText = GetText("TextTeamExp");
        HeroImageBox = GetImage("HeroImageBox");
        TextUsername = GetText("TextUsername");
        //username = controls.GetInputFiled("InputFieldUsername");
        //password = controls.GetInputFiled("InputFieldPassword");
        base.Init();
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnSystemSettingClick()
    {
        Debug.Log("OnSystemSettingClick");
        //Hide();
        PanelManager.me.Get((int) PanelId.PanelSetting).Show();
        PanelManager.me.Get((int) PanelId.PanelTeamInfo).Hide();
    }

    //将礼包兑换短暂的换成点金手的页面进行调试
    public void OnGiftExchangeClick()
    {
        Debug.Log("OnGiftExchangeClick,将礼包兑换短暂的换成点金手的页面进行调试");
        PanelManager.me.Get((int) PanelId.PanelTeamInfo).Hide();
        PanelManager.me.Get((int) PanelId.PanelGetMoney).Show();
    }

    public void OnButtonChangeUsernameClick()
    {
        Debug.Log("OnButtonChangeUsernameClick");
        PanelManager.me.Get((int) PanelId.PanelChangeUsername).Show();
        Debug.Log("Ok");
        //PanelManager.me.Get((int)PanelId.PanelChangeUserName).Show();
        //PanelManager.me.Get((int)PanelId.PanelTeamInfo).Hide();
    }

    public void OnChangeImageFrameClick()
    {
        Debug.Log("OnChangeImageFrameClick");
        // PanelManager.me.Get((int) PanelId.PanelChangeIconFrame).Show();
        PanelChangeIconFrame.SetActive(true);
    }

    public void OnCancelClick()
    {
        Hide();
    }


    public override void Refresh()
    {
        base.Refresh();
        var teamIconFrame = Gamedata.me.userinfo.TeamIconFrame;
        TextUsername.text = Gamedata.me.userinfo.Username;
        TeamLevelText.text = Gamedata.me.userinfo.TeamLevel.ToString();
        TeamExpText.text = Gamedata.me.userinfo.TeamExp.ToString();
        LevelLimitText.text = Gamedata.me.userinfo.TeamLevel.ToString();
        UserIDText.text = Gamedata.me.userinfo.Id.ToString();
        Debug.Log("Change Team Icon Frame");
    }

    //public override void OnShow()
    //{
    //    TeamLevelText.text = Gamedata.me.userinfo.TeamLevel.ToString();
    //    TeamExpText.text = Gamedata.me.userinfo.TeamExp.ToString();
    //    LevelLimitText.text = Gamedata.me.userinfo.TeamLevel.ToString();
    //    UserIDText.text = Gamedata.me.userinfo.Id.ToString();
    //    //Gamedata gamedata = new Gamedata();
    //    //TeamLevelText.text = gamedata.userinfo.TeamLevel.ToString();
    //    //TeamExperienceText.text = gamedata.userinfo.TeamExp.ToString();
    //    //HeroLevelUpperLimitText.text = gamedata.userinfo.TeamLevel.ToString();
    //    //UsernameIDText.text = gamedata.userinfo.Id.ToString();

    //    base.OnShow();
    //}
}