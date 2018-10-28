using Assets.Script.Frame;
using UnityEngine;

public class PanelChangeIconFrame : PanelBase
{
    public GameObject PanelChangeIconFrameGameObject;
    //public PanelBase panelChoiceImageFrame;
    // Use this for initialization
    public void Start()
    {
    }

    /* public void Awake()
    {
      GameObject prefabs = Resources.Load<GameObject>("Panels/PanelChoiceImageFrame");
       panelChoiceImageFrame = Instantiate<GameObject>(prefabs).GetComponent<PanelBase>();
       panelChoiceImageFrame.transform.parent = transform;
   }*/
    // Update is called once per frame
    private void Update()
    {
    }

    public void OnChangeIconButtonClick()
    {
    }

    public void OnButtonCancelClick()
    {
        PanelChangeIconFrameGameObject.SetActive(false);
    }

    /* public override void Show()
    {
        base.Show();
        controls.GetPanelBase("PanelChoiceImageFrame").Show();
    }*/
}