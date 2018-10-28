// UiControlRegistor.cs
// Author: Iann


using UnityEngine;


namespace Assets.Script.Frame
{

    /// <summary>
    /// 界面控件注册器的基类
    /// </summary>
    class UiControlRegistor : MonoBehaviour
    {
        /// <summary>
        /// 指定的界面控件容器
        /// </summary>
        public UiControlContainer PointedContainer = null;

        /// <summary>
        /// 注册后立即删除本注册器， 用于多次加载的选项
        /// </summary>
        public bool removeThisWhenAwake = false;

        /// <summary>
        /// 唤醒函数， 我们的注册就是在这个函数中实现的
        /// </summary>
        public virtual void Awake()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiBehaviour(" + name + ").Awake");
#endif
            container.RegistObject(name, gameObject);
            if (removeThisWhenAwake)
            {
                DestroyImmediate(this);
            }
        }

        /// <summary>
        /// 销毁函数， 反注册写在这个函数中
        /// </summary>
        public virtual void Destroy()
        {
#if USE_LOG && LOG_FRAME_UI
            Debug.Log("UiBehaviour(" + name + ").Destroy");
#endif
            if (!removeThisWhenAwake)
                container.UnRegistObject(name);
        }

        /// <summary>
        ///  如果制定了目标容器就返回制定的目标容器， 
        ///  否则依次向跟节点查找容器， 返回路径上的第一个容器作为目标容器
        /// </summary>
        public UiControlContainer container { get { return PointedContainer ? 
            PointedContainer : transform.GetComponentInParent<UiControlContainer>(); } }
    }
}