// AssetManager.cs
// Author: Iann

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Script.Frame
{
    /// <summary>
    /// 资源的位置
    /// </summary>
    public enum AssetLocation
    {
        Normal,     // 普通资源， 存储在Unity项目目录中但不在Resources目录下的资源。
        Resources,  // 动态资源， 存储在Unity项目下的Resources目录下的资源。
        Bundle,     // 导出的资源， 从Unity项目中导出成Asset Bundle的资源。
        Externals,  // 外部资源， Unity项目外部的资源。
    }

    /// <summary>
    /// 资源配置表数据（从资源名字到资源位置的映射）
    /// </summary>
    public class AssetConfigTableData
    {
        public string AssetName { get; set; } // 资源的名字

        public AssetLocation AssetLocation { get; set; } // 资源的位置
    }

    /// <summary>
    /// 导出资源配置表
    /// 资源名， 资源路径， 集合名直接的映射关系
    /// </summary>
    public class BundleAssetConfigTableData
    {
        public string AssetName { get; set; } // 资源的名字

        public string BundleName { get; set; } // Bundle名字

        public string AssetPath { get; set; } // 资源路径
    }

   

    /// <summary>
    /// 资源配置类
    /// 主要提供资源名称到资源位置的映射及资源配置表的加载
    /// </summary>
    public class AssetConfig : Singleton<AssetConfig>
    {
        AssetConfigTableData[] datas;

        /// <summary>
        /// 读取资源配置表
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            datas = null;
            datas = Database.me.GetAll<AssetConfigTableData>();
            return true;
        }
        
        /// <summary>
        /// 资源名称到资源位置的映射
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <returns>资源位置</returns>
        public AssetLocation AssetNameToAssetLocation(string assetName)
        {
            foreach (AssetConfigTableData it in datas)
            {
                if (it.AssetName == assetName)
                {
                    return it.AssetLocation;
                }
            }
            return AssetLocation.Resources;
        }
    }

    /// <summary>
    /// 资源集合配置类
    /// 主要提供资源名到集合名之间的映射
    /// </summary>
    public class BundleAssetConfig : Singleton<BundleAssetConfig>
    {
        BundleAssetConfigTableData[] datas;

        /// <summary>
        /// 资源集合配置表
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            datas = null;
            datas = Database.me.GetAll<BundleAssetConfigTableData>();
            return true;
        }

        /// <summary>
        /// 取得所有资源集合数据
        /// </summary>
        /// <returns></returns>
        public BundleAssetConfigTableData[] All()
        {
            return datas;
        }


        /// <summary>
        /// 资源名称到集合名称的映射
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <returns>集合名称</returns>
        public string AssetNameToBundleName(string assetName)
        {
            foreach (var it in datas)
            {
                if (it.AssetName == assetName)
                    return it.BundleName;
            }
            return null;
        }      
    }

    /// <summary>
    /// 资源提供接口
    /// 这个类的接口是按资源的种类确定的
    /// </summary>
    public interface IAssetProvider
    {
        /// <summary>
        /// 获取精灵
        /// </summary>
        /// <param name="altasName">精灵资源（图集Altas)的名字</param>
        /// <returns>精灵数组</returns>
        Sprite[] GetAltas(string altasName);

        /// <summary>
        /// 获取界面Panel
        /// </summary>
        /// <param name="panelName">界面名称</param>
        /// <returns>界面的Panel</returns>
        PanelBase GetPanel(string panelName);
    }

    /// <summary>
    /// 资源管理类
    /// 这个类实现IResLoader接口。
    /// 这个类的主要功能是屏蔽不同位置的资源的不同的加载方式。
    /// </summary>
    public class AssetManager : Singleton<AssetManager>, IAssetProvider
    {
        ResourcesAssetProvider resourcesAssetProvider = new ResourcesAssetProvider();
        BundleAssetProvider bundleAssetProvider = new BundleAssetProvider();

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <returns></returns>
        public IEnumerator Load()
        {
            yield return bundleAssetProvider.Load();
        }


        public Sprite[] GetAltas(string altasName)
        {
            AssetLocation loc = AssetConfig.me.AssetNameToAssetLocation(altasName);
            if (loc == AssetLocation.Resources)
            {
                return resourcesAssetProvider.GetAltas(altasName);
            }
            else if (loc == AssetLocation.Bundle)
            {
                return bundleAssetProvider.GetAltas(altasName);
            }
            return null;
        }

        public PanelBase GetPanel(string panelName)
        {
            AssetLocation loc = AssetConfig.me.AssetNameToAssetLocation(panelName);
            if (loc == AssetLocation.Resources)
            {
                return resourcesAssetProvider.GetPanel(panelName);
            }
            else if (loc == AssetLocation.Bundle)
            {
                return bundleAssetProvider.GetPanel(panelName);
            }
            return null;
        }

    }

    /// <summary>
    /// 动态资源（Resource)提供类
    /// </summary>
    public class ResourcesAssetProvider: IAssetProvider
    {

        public Sprite[] GetAltas(string altasName)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Altas/" + altasName);
            return sprites;
        }

        public PanelBase GetPanel(string panelName)
        {
            PanelBase panel = Resources.Load<PanelBase>("Panels/" + panelName);
            return panel;
        }
    }

    /// <summary>
    /// 集合资源（Resource）提供类
    /// </summary>
    public class BundleAssetProvider : IAssetProvider
    {
        public string urlbase = "file:///" + Application.dataPath + "/../../Bundles/";
        Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
        AssetBundleManifest manifest;

        /// <summary>
        /// 加载资源集合
        /// </summary>
        /// <returns></returns>
        public IEnumerator Load()
        {
            yield return DownloadManifest();
            string[] allToLoad = GetAllBundlesOrderByDependency(manifest);
            foreach (string toLoad in allToLoad)
            {
                Debug.Log(toLoad);
                string url = urlbase + toLoad;
                Hash128 hash = manifest.GetAssetBundleHash(toLoad);
                WWW www = WWW.LoadFromCacheOrDownload(url, hash);
                yield return www;
                Debug.Log(www.error);
                AssetBundle bundle = www.assetBundle;
                bundles.Add(toLoad, bundle);
            }
            
        }

        /// <summary>
        /// 获取所有要加载的结合（AssetBundle），按加载所需的顺序排序。
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns>排序后的结合名称列表</returns>
        string[] GetAllBundlesOrderByDependency(AssetBundleManifest manifest)
        {            
            string[] allToOrder = manifest.GetAllAssetBundles();
            List<string> ordered = new List<string>();
            foreach (string dep in allToOrder)
            {
                OrderOneBundleCycle(manifest, dep, ref ordered);
            }
            return ordered.ToArray();           
        }

        void OrderOneBundleCycle(AssetBundleManifest manifest, string name, ref List<string> orderedList)
        {
            if (orderedList.Contains(name))
            {
                return;
            }

            string[] allToOrder = manifest.GetDirectDependencies(name);
            if (allToOrder.Length == 0)
            {
                orderedList.Add(name);
            }

            foreach(string dep in allToOrder)
            {
                OrderOneBundleCycle(manifest, dep, ref orderedList);
            }
        }

        /// <summary>
        /// 下载集合根信息
        /// </summary>
        /// <returns></returns>
        IEnumerator DownloadManifest()
        {
            string url = urlbase + "Bundles";
            WWW www = new WWW(url);
            yield return www;
            Debug.Log(www.error);
            AssetBundle bundle = www.assetBundle;
            manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        public Sprite[] GetAltas(string altasName)
        {
            string bundleName = BundleAssetConfig.me.AssetNameToBundleName(altasName);
            AssetBundle bundle = bundles[bundleName];
            return bundle.LoadAllAssets<Sprite>();
        }

        public PanelBase GetPanel(string panelName)
        {
            string bundleName = BundleAssetConfig.me.AssetNameToBundleName(panelName);
            AssetBundle bundle = bundles[bundleName.ToLower()];
            GameObject panelObj = bundle.LoadAsset<GameObject>(panelName);
            return panelObj.GetComponent<PanelBase>();
        }
    }


}
