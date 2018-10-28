// PanelBase.cs
// Author: Iann

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Frame
{
    /// <summary>
    /// 界面的基类
    /// 定义了诸如Init、Show、Hide、Refresh等可重写的流程函数
    /// </summary>
    [RequireComponent(typeof(UiControlContainer))]
    public class PanelBase : MonoBehaviour
    {
        /// <summary>
        /// 控件容器
        /// </summary>
        public UiControlContainer controls { get { return GetComponent<UiControlContainer>();  } }

        /// <summary>
        /// 所有SubPanel
        /// </summary>
        public PanelBase[] subPanels { get { return controls.panelbases.Values.ToArray<PanelBase>(); } }

        /// <summary>
        /// 根据名字获取Button控件
        /// </summary>
        /// <param name="name">Button的名字</param>
        /// <returns>Button控件</returns>
        public virtual Button GetButton(string name)
        {
            return controls.GetButton(name);
        }

        /// <summary>
        /// 根据名字获取Button控件
        /// </summary>
        /// <param name="name">Button的名字</param>
        /// <returns>Button控件</returns>
        public virtual Text GetText(string name)
        {
            return controls.GetText(name);
        }

        /// <summary>
        /// 根据名字获取Image控件
        /// </summary>
        /// <param name="name">Image的名字</param>
        /// <returns>Image控件</returns>
        public virtual Image GetImage(string name)
        {
            return controls.GetImage(name);
        }

        /// <summary>
        /// 根据名字获取SubPanel控件
        /// </summary>
        /// <param name="name">SubPanel的名字</param>
        /// <returns>SubPanel控件</returns>
        public virtual PanelBase GetSubPanel(string name)
        {
            return controls.GetPanelBase(name);
        }

        /// <summary>
        /// 界面的初始化函数
        /// 一般重载这个函数的主要目的是初始化界面控件
        /// 注意控件的注册是在Awake函数中进行的，保证在调用container.get的时候控件注册器的Awake已经调用
        /// </summary>
        public virtual void Init()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("Init Panel " + name + ".");
#endif

            bool active = gameObject.activeSelf;
            gameObject.SetActive(true);  // 确保初始化前UiControlRegister的Awake已经调用
            foreach (PanelBase subPanel in subPanels)
            {
                subPanel.Init();
            }
            gameObject.SetActive(active);
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        public virtual void Show()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("PaneBase(" + name + ").Show");
#endif
            gameObject.SetActive(true);
            //OnShow();
            Refresh();
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        public virtual void Hide()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("PaneBase(" + name + ").Hide");
#endif
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public virtual void Refresh()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("PanelBase(" + name + ").Refresh");
#endif
        }

    }
    
    
}
