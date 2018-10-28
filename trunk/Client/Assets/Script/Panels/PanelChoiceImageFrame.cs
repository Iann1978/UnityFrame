using Assets.Script.Frame;
using UnityEngine;
using UnityEngine.UI;

public class PanelChoiceImageFrame : PanelBase
{
    public Image iconFrame;
    public Image imgBox;
    // Use this for initialization
    private void Start()
    {
    }

    public void OnChangeIconButtonClick()
    {
        iconFrame.sprite = imgBox.sprite;
        Debug.Log("更改图hhh片");
    }

    public void OnButtonClick()
    {
        Debug.LogError("更改图片");
        var reqChangeTeamIconFrame = new ReqChangeTeamIconFrame();
        reqChangeTeamIconFrame.Userid = Gamedata.me.userinfo.Id;
        reqChangeTeamIconFrame.TeamIconFrame = 15;
        Net.me.Send(reqChangeTeamIconFrame);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}