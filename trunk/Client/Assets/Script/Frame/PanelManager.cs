// PanelManager.cs
// Author: Iann


using System.Collections.Generic;
using UnityEngine;


namespace Assets.Script.Frame
{
    /// <summary>
    /// 界面管理类
    /// 本类提供了界面的加载，显示，隐藏，（关闭）功能
    /// 动画过度暂未实现
    /// 导航功能暂未实现
    /// </summary>
    public class PanelManager : Singleton<PanelManager>
    {
        // 界面的实例化函数
        public delegate PanelBase FuncInstanceUiBase(PanelBase prefab);
        public FuncInstanceUiBase InstanceUiBase;

        // prefab 容器
        private readonly Dictionary<int, PanelBase> prefabs = new Dictionary<int, PanelBase>();

        // 已经加载的界面容器
        private readonly Dictionary<int, PanelBase> uis = new Dictionary<int, PanelBase>();

        
        public void Regist(int uiid, PanelBase ui)
        {
            //types.Add(typeof(T), uiid);
            uis.Add(uiid, ui);
        }
        
        /// <summary>
        ///  注册界面的Prefab
        /// </summary>
        /// <param name="uiid">界面的类型id</param>
        /// <param name="prefab">界面的prefab</param>
        public void RegistPrefab(int uiid, PanelBase prefab)
        {
            if (prefab == null)
            {
                Debug.LogError(string.Format("PanelManager.RegistPrefab({0},{1}) Failed !!!", uiid, prefab));
                return;
            }

#if USE_LOG
            Debug.Log(string.Format("PanelManager.RegistPrefab({0},{1}", uiid, prefab));
#endif 
            
            prefabs.Add(uiid, prefab);
        }

        /// <summary>
        /// 获取界面
        /// </summary>
        /// <param name="uiid">界面的类型id</param>
        /// <returns></returns>
        public PanelBase Get(int uiid)
        {
            //如果界面已经加载过， 返回已经加载过的界面
            if (uis.ContainsKey(uiid))
                return uis[uiid];
            
            // 加载界面
            if (prefabs.ContainsKey(uiid))
            {
                if (prefabs[uiid] == null)
                {
                    Debug.LogError((PanelId)uiid);
                }
                var ui = InstanceUiBase(prefabs[uiid]);
                uis.Add(uiid, ui);
                ui.gameObject.SetActive(true); // 确保初始化前UiControlRegister的Awake已经调用
                ui.name = ui.name.Substring(0, ui.name.IndexOf("(Clone)"));                
                ToTop(ui);
                ui.Init();
                AdjustPanelPosition(ui);
                return ui;
            }

            return null;
        }

        /// <summary>
        /// 调整Panel的位置布局
        /// </summary>
        /// <param name="ui">待调整的Panel</param>
        void AdjustPanelPosition(PanelBase ui)
        {
            ui.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").transform);
            var rt = ui.GetComponent<RectTransform>();
            var op = ui.GetComponent<RectTransformAdjustor>();
            if (op != null)
            {
                op.Adjust();
            }
            else
            {
                ui.transform.localPosition = Vector3.zero;
                ui.transform.localScale = Vector3.one;
            }
        }

        /// <summary>
        /// 将Panel放到最前面
        /// </summary>
        /// <param name="ui">待调整的Panel</param>
        void ToTop(PanelBase ui)
        {
            ui.transform.SetAsLastSibling();
        }

        /// <summary>
        /// 隐藏所有界面
        /// </summary>
        public void HideAll()
        {
            foreach (var ui in uis.Values)
                ui.Hide();
        }

        /// <summary>
        /// 显示所有界面
        /// </summary>
        public void RefreshAll()
        {
            foreach (var ui in uis.Values)
                ui.Refresh();
        }
    }
}