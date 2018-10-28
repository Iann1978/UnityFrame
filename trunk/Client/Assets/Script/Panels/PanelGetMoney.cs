using Assets.Script.Frame;
using UnityEngine;
using UnityEngine.UI;

public class PanelGetMoney : PanelBase
{
    public Text CostDiamondText;
    private float CurTime;
    public Text GetMoneyCountText;
    public GameObject PanelGetMoneyResult;
    public Text ResultCostDiamondText;
    public Text ResultGetMoneyCountText;
    public Text ResultRandomTimesText;
    public Text TodayCountNumberText;
    public GameObject useButton;

    //public Text TeamExpText;
    //public GameObject PanelChangeIconFrame;
    //public Image HeroImageBox;

    /// <summary>
    /// </summary>
    public override void Init()
    {
        TodayCountNumberText = GetText("TodayCountNumberText");
        CostDiamondText = GetText("CostDiamondText");
        GetMoneyCountText = GetText("GetMoneyCountText");
        ResultRandomTimesText = GetText("ResultRandomTimesText");


        //TeamExpText = GetText("TextTeamExp");
        // HeroImageBox = GetImage("HeroImageBox");
        //username = controls.GetInputFiled("InputFieldUsername");
        //password = controls.GetInputFiled("InputFieldPassword");
        base.Init();
    }

    public void OnCancelButtonClick()
    {
        Debug.Log("OnCancelButtonClick");
        PanelManager.me.Get((int) PanelId.PanelGetMoney).Hide();
        PanelGetMoneyResult.SetActive(false);
        useButton.SetActive(true);

        Debug.Log("Ok");
    }

    public void OnMoneyButtonClick()
    {
        Debug.Log("OnMoneyButtonClick");
        // var csvStreamReader = new CsvStreamReader(@"C:\Users\seven\Desktop\春风十里不如你\trunk\Csv\AssetConfig2.csv");

        // Debug.Log(csvStreamReader[1, 1]);
        //PanelManager.me.Get((int)PanelId.PanelGetMoneyResult).Show();
        useButton.SetActive(false);
        PanelGetMoneyResult.SetActive(true);
        //测试一下下
        //暴击。。。。
        var randomTimes = Random.Range(0, 5);
        ResultRandomTimesText.text = randomTimes.ToString();
        Gamedata.me.userinfo.Coin = Gamedata.me.userinfo.Coin + int.Parse(GetMoneyCountText.text)*randomTimes;
        Debug.Log(Gamedata.me.userinfo.Coin);
        //Gamedata.me.userinfo.Diamond = 30;
        if (Gamedata.me.userinfo.Diamond >= 10)
            Gamedata.me.userinfo.Diamond -= int.Parse(CostDiamondText.text);
        else
            Debug.Log("主人钻石不够呢，充充钻石可好？");
        //int addMoney = int.Parse(TodayCountNumberText.text);
        //int originMoney = Gamedata.me.userinfo.Coin;
        //Gamedata.me.userinfo.Coin = originMoney + addMoney;
        ResultCostDiamondText.text = CostDiamondText.text;
        ResultGetMoneyCountText.text = (int.Parse(GetMoneyCountText.text)*randomTimes).ToString();
        PanelManager.me.Get((int) PanelId.PanelMain).Refresh();

        Debug.Log("Ok");
    }

    /* public override void OnShow()
    {
        TeamLevelText.text = Gamedata.me.userinfo.TeamLevel.ToString();
        TeamExpText.text = Gamedata.me.userinfo.TeamExp.ToString();
        LevelLimitText.text = Gamedata.me.userinfo.TeamLevel.ToString();
        UserIDText.text = Gamedata.me.userinfo.Id.ToString();
        //Gamedata gamedata = new Gamedata();
        //TeamLevelText.text = gamedata.userinfo.TeamLevel.ToString();
        //TeamExperienceText.text = gamedata.userinfo.TeamExp.ToString();
        //HeroLevelUpperLimitText.text = gamedata.userinfo.TeamLevel.ToString();
        //UsernameIDText.text = gamedata.userinfo.Id.ToString();

        base.OnShow();
    }
    */
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        CurTime += Time.deltaTime;
        if (CurTime > 3.0f)
        {
            useButton.SetActive(true);
            PanelGetMoneyResult.SetActive(false);
            CurTime = 0.0f;
        }
    }
}