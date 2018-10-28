using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using UnityEngine.UI;

public class PanelLoading : PanelBase
{
    Text txProcess;
    //public override void Init()
    //{
    //    base.Init();

    //    RegistEvents(new int[] { (int)EventId.EventProgress });
    //    txProcess = GetText("TextProcess");
    //}

    //public override void ProcessEvent(IEvent ievt)
    //{
    //    if (ievt.evtId == (int)EventId.EventProgress)
    //    {
    //        ProcessProgress(ievt as EventProgress);
    //        return;

    //    }

    //    base.ProcessEvent(ievt);
    //}

    void ProcessProgress(EventProgress evt)
    {
        int intProgress = (int)(evt.progress * 100);
        txProcess.text = intProgress.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
