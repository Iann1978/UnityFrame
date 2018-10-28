using System;
using Assets.Script.Frame;
using ReadFromCsv;
using UnityEngine;
using UnityEngine.UI;

public class PanelGetVITCOPY : PanelBase
{
    private static readonly CsvStreamReader csv =
        new CsvStreamReader("C:\\Users\\woopqww111\\Desktop\\Change.csv");

    private readonly int MaxCol = csv.ColCount;
    private readonly int MaxRow = csv.RowCount;
    private int rows = 2;
    public Text TextCostDiamond;
    public Text TextGetVIT;
    public Text TextTimes;
    private int Times = 2;

    public override void Init()
    {
        TextCostDiamond = GetText("TextCostDiamond");
        TextGetVIT = GetText("TextGetVIT");
        TextTimes = GetText("TextTimes");
        if (GetCol("VIT") != -2)
        {
            TextGetVIT.text = csv[MaxRow - 1, GetCol("VIT")];
            Debug.Log(TextGetVIT.text);
        }

        Debug.Log("没有相应的数据传出");
        if (GetCol("Diamond") != -2)
        {
            TextCostDiamond.text = csv[rows, GetCol("Diamond")];
            Debug.Log(TextCostDiamond.text);
        }

        else
            Debug.Log("没有相应的数据传出");
        PanelManager.me.RefreshAll();
    }

    // Use this for initialization
    private void Start()
    {
        TextTimes.text = "0";
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnConfirmButtonClick()
    {
        TextCostDiamond = GetText("TextCostDiamond");
        TextGetVIT = GetText("TextGetVIT");
        TextTimes = GetText("TextTimes");
        if (GetCol("VIT") != -2)
            TextGetVIT.text = csv[MaxRow - 1, GetCol("VIT")];
        else
            Debug.Log("没有相应的数据传出");
        if (GetCol("Diamond") != -2)
            TextCostDiamond.text = csv[rows, GetCol("Diamond")];
        else
            Debug.Log("没有相应的数据传出");

        //测试服务器好使不
        Gamedata.me.userinfo.Diamond = 200;
        Gamedata.me.userinfo.Coin = 200;
        Gamedata.me.userinfo.Vigour = 200;
        Debug.Log(TextTimes.text);

        if ((int.Parse(TextTimes.text) < Convert.ToInt32(csv[MaxRow, GetCol("Times")])) &&
            (Gamedata.me.userinfo.Diamond >= Convert.ToInt32(TextCostDiamond.text)))

        {
            Gamedata.me.userinfo.Vigour += int.Parse(TextGetVIT.text);


            TextTimes.text = csv[rows, GetCol("Times")];
            Gamedata.me.userinfo.Diamond -= int.Parse(TextCostDiamond.text);
            TextCostDiamond.text = csv[++rows, GetCol("Diamond")];
            PanelManager.me.RefreshAll();
        }
    }

    public void OnCancelButtonClick()
    {
        Debug.Log("取消");
        PanelManager.me.Get((int) PanelId.PanelGetVIT).Hide();
        //取消，让其自己消失即可
    }

    /// <summary>
    ///     返回是否找到对应的字符串的列数
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    private int GetCol(string title)
    {
        for (var i = 1 ; i < csv.ColCount; i++)
            if (title.Equals(csv[1, i]))
                return i;


        return -2;
    }
}