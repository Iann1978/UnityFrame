// SpriteFactoy.cs
// Author: Iann

using UnityEngine;
using Assets.Script.Frame;

namespace Assets.Script
{

    /// <summary>
    /// 精灵工厂
    /// 这是个逻辑层的类， 提供各种逻辑中需要的精灵
    /// </summary>
    public class SpriteFactory : Singleton<SpriteFactory>
    {
        public Sprite Get(string resFunc, int id)
        {
            string altasName = "Icon";
            string spriteName = string.Format("{0}-{1:D4}", resFunc, id);
            Sprite[] sprites = AssetManager.me.GetAltas(altasName);
            foreach (Sprite sp in sprites)
            {
                if (sp.name == spriteName)
                    return sp;
            }
            return null;
        }
    }

    public class PanelFactory : Singleton<PanelFactory>
    {
        public PanelBase Get(string panelName)
        {
            return AssetManager.me.GetPanel(panelName);
        }
    }
}
