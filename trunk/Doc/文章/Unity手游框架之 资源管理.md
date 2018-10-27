 	
#<small>Unity手游框架<small>之</small></small> 资源管理

@(Frame)[Unity, 资源管理, AssetBundle, Frame]


##引子
资源管理是做什么用的，让我们先来看一段代码

```
public void SetHeroInfo(HeroInfo heroInfo)
{
	GetControls();
	int heroResId = heroInfo.Id & 0xffff;
	HeroTableData heroTableData = Database.me.GetById<HeroTableData>(heroResId);
	UpdateEffect = EffectFactory.me.Get("UIEffect-HeroUpdateEffect", heroResId);
	UpdateSound = SoundFactory.me.Get("UISound-HeroUpdateSound", heroResId);
	ImageHeroHead.sprite = SpriteFactory.me.Get("HeroHead", heroResId);
	ImageHeroQuality.sprite = SpriteFactory.me.Get("HeroQuality", heroInfo.Quality);
	ImageHeroOccupation.sprite = SpriteFactory.me.Get("HeroOccupation", heroInfo.Occupation);
	TextHeroName.text = heroTableData.HeroName;
	TextHeroLevel.text = "Lv." + heroInfo.Level.ToString();
	HeroLevel.gameObject.SetActive(true);
	SliderChips.gameObect.SetActive(false);
	
}
```

在SetHeroInfo这个函数中可以看到不同资源使用的例子：

*	取得英雄的升级特效：```UpdateEffect = EffectFactory.me.Get("UIEffect-HeroUpdateEffect", heroResId);```
*	取得英雄的升级音效：```UpdateSound = SoundFactory.me.Get("UISound-HeroUpdateSound", heroResId);```
*	取得英雄的头像：```ImageHeroHead.sprite = SpriteFactory.me.Get("HeroHead", heroResId);```

这些资源又以不同的形式存与不同的位置。例如我们假定升级特效以prefab的形式存储于Resources目录下（Assets/Resources/Effect/UIEffect/HeroUpdate-0002.prefab），升级音效以mp3格式存储于Resources目录下（Assets/Resources/Sound/UISound/HeroUpdate-0002.mp3）， 英雄头像以Altas的形式存储与名字叫Icon的AssetBundle下的名字叫Icon的Altas内，而这个AssetBundle位于http:///192.168.1.21/Bundles下。可见这些资源存储的位置、组织的方式、加载的方式都不尽相同，但我们在这里却使用了简洁且近乎相同的方式获得了这些资源。这是如何做到的呢？

事实上，上面这段文字就向我们解释了资源管理的作用：有效的管理和加载资源，并以简洁接口提供个上层逻辑。

回答完资源管理的作用，下面我们就要讲解如何实现资源管理了， 这里给出本文的思路。

1. 整理Unity资源的分类，并了解不同资源的特性。
2. 从现有的项目代码及个人经验中抽象出上层接口的形式。
3. 从上面两条推演出各个中间层（这个中间层就是资源管理）的设计及实现。

如果把上面的第一条看成是一个题目的已知项，第二条看成是这个题目的要求的解，那么这第三条就是这求解的过程。

##Unity资源的分类
*	按资源的位置及加载方式分类（AssetLocation):	
	*	普通资源（Normal）		这类资源存储这工程中Resources目录外， 运行是存储于本地，自动加载。引用方式为直接引用，加载与引用都不需要参数
	*	动态资源（Resources） 这类资源存储与工程中的Resources目录下， 运行时存储于本地，运行时动态加载。常用的加载函数又Resources.Load， 加载时需要知道资源类型（AssetType）和资源名称（AssetName）等参数。
	*	集合资源（Bundle） 这类资源是在项目中用BuildAssetBundles导出的资源，以AssetBundle形式存储于本地或网络中。加载时使用WWW方式加载，加载时需要Bundle存放的url路径，版本号等参数。 引用这类资源时使用AssetBundle内的一些API，引用资源要知道资源类型（AssetType），资源名字（AssetName），资源所在的Bundle名成（BundleName）等。
	*	外部资源（External） 这类资源存储在项目外， 运行时可能存储于本地也可能存储于网络中， 以自定义的方式加载和引用， 加载及引用时所需的参数也需要自己定义。
	
*	按资源的种类划分（AssetType）:
	*	图片（png、jpg、gif...）
	*	预制体（prefab）
	*	声音（mp3、wav...）
	*	... ...
	
*	按资源最终提供的功能划分：升级特效、技能特效、界面、头像、技能图标等。

##	上层接口的提供形式
上层接口是由各个逻辑功能直接调用的接口，在上层接口的设计上要尽量屏蔽资源的加载、存放等与逻辑无关的信息。下面我们先看几个在项目中搜罗的一些接口，然后抽象出本文中用到的上层接口的形式。

*	获取升级特效：```GameObject GetHeroUpdateEffect(int heroId);```
*	获取技能特效：```GameObject GetSkillEffect(int skillId);```
*	获取英雄头像：```Sprite GetHeroHead(int heroId);```

从上面的接口可以看到这些接口是与最终逻辑紧密相关的，同时我们还可以看到逻辑层为取得这些资源提供了要获取的资源的类型（XType），及资源的相关参数（funcName params）。综上给出本文的上层接口的形式：

*XType XXFactory(string funcName, params)*

例如：	```GameObject EffectFactory.Get(string funcName, int index);```
```Sprite SpriteFactory.Get(string funcName, int index);```
		
## 底层接口的提供形式
底层接口是使用系统及Unity提供的API实现的方便上层接口调用的接口，是系统API到上层接口直接的过度，底层接口直接也是有层次关系的，底层接口的设计原则与设计模式中一致。这里直接给出底层接口的设计：

* IAssetProvider 以interface形式存在，用以规范化资源的提供形式。
	
	```
	public interface IAssetProvider
	{
		Sprite[] GetAltas(string altasName);
		GameObject GetPrefab(string prefabName);
		PanelBase GetPanel(string panelName);
		... ...
	}
	
	```
* AssetManager 通过管理和调用不同的IAssetProvider的实例来实现的IAssetProvider接口，共上层调用。
 
 	```
 	public class AssetMananger :Singleton<AssetManger> , IAssetProvider
 	{
 		ResourceAssetProvider resourceAssetProvider;
 		BundleAssetProvider bundleAssetProvider;
 		
 		Sprite[] GetAltas(string altasName);
		GameObject GetPrefab(string prefabName);
		PanelBase GetPanel(string panelName);
		... ...
 	}
 	```
* ResourcesAssetProvider 动态资源（AssetLocation为Resouces）提供者，使用动态资源实现的IAssetProvider接口。

	```
	public class ResourcesAssetProvider : IAssetProvider
	{
		Sprite[] GetAltas(string altasName);
		GameObject GetPrefab(string prefabName);
		PanelBase GetPanel(string panelName);
		... ...
	}
	```
* BundleAssetProvider 集合资源（AssetLocation为Bundle）提供者，使用集合资源实现的IAssetProvider接口。


	```
	public class BundleAssetProvider : IAssetProvider
	{
		Sprite[] GetAltas(string altasName);
		GameObject GetPrefab(string prefabName);
		PanelBase GetPanel(string panelName);
		... ...
	}
	
	```

* AssetConfig 资源配置表，实现由资源名称（AssetName）到资源位置（AssetLocation）的映射。


	```
	public class AssetConfig : Singleton<AssetConfig>
	{
		public AssetLocation AssetNameToAssetLocation(string assetName);
	}
	
	```

* BundleAssetConfig 集合资源配置表， 实现由资源名称（AssetName）到资源集名称（BundleNmae）的映射。


	```
	public class BundleAssetConfig : Singleton<BundleAssetConfig>
	{
		public string AssetNameToBundleName(string assetName);
	}
	
	```
## 底层接口是如何设计出来的

我们可以通过自顶向下的方式设计我们的接口，当然如果最初的设计我们不满意，我们也可以通过代码重构来修改我们的设计。即是自顶向下设计那我们就从最上层开始了。文章的开篇给出了一段某功能使用的代码，我们看到里面有个函数SpriteFactory.Get，这也恰好是本文给出的资源管理的上层接口，那我们就首先实现这个接口。

	```
	public class SpriteFactory :Singtong<SpriteFactory>
	{
		public Sprite Get(string funcName, int index)
		{
			// 取得altas的资源名称
			string altasName = "Icon";
			
			// 取得图集
			Sprite[] altas = AssetManager.me.GetAltas(altasName);
			
			// 取得精灵的名字
			string spriteName = string.Format("{0}-{1:D4}", funcName, index);
			
			// 从图集从查找精灵
			foreach (Sprite sprite in altas)
			{
				if (sprite.name == spriteName)
					return sprite;
			}
			
			return null;
		}
	}
	```
接下来实现上面接口中的AssetManager.GetAltas这个函数。
	
	```
	public class AssetManager : Sington<AssetManager> , IAssetProvider
	{
		ResourceAssetProvider resourceAssetProvider;
 		BundleAssetProvider bundleAssetProvider;
 		
 		public Sprite[] GetAltas(string altasName)
 		{
 			AssetLocation loc = AssetConfig.me.AssetNameToAssetLocation(altasName);
 			if (loc == AssetLocation.Resources)
 			{
 				return resourceAssetProvider.GetAltas(altasName);
 			}
 			else if (loc == AssetLocation.Bundle)
 			{
 				return bundleAssetProvider.GetAltas(altasName);
 			}
 			return null;
 		}
		
	}
	
	```
从上面代码可以看到AssetManager这个类其实只做了简单的分拣工作，这个类的功能单一，这符合类设计的原则。在分拣过程中遇到两个问题。1 分拣的规则， 这个问题交给AssetConfig来解决，而资源的真正获取资源则交给了其他的资源提供者ResourceAssetProvider和BundleAssetProvider。分拣的规则是由AssetConfig来处理的，我们这里使用配置表的方式实现。这个问题比较简单就不贴代码到文章中了，有问题可以参看附录中的源代码。下面看下ResourcesAssetProvider.GetAltas和AssetBundleProvider.GetAltas是如何实现的。

	```
	public class ResourceAssetProvider : IAssetProvider
	{
		public Sprite[] GetAltas(string altasName)
		{
			Sprite[] altas = Resources.LoadAll("Altas/"+altasName);
			return altas;
		}
		... ...
	}
	```

	```
	public class BundleAssetProvider : IAssetProvider
	{
		Directionary<string, AssetBundle> bundles;
		
		public Sprite[] GetAltas(string altasName)
		{
			string bundleName = BundleAssetConfig.me.AssetNameToBundleName(altasName);
			AssetBundle assetBundle = bundles[bundleName.ToLower];
			Sprite[] altas = assetBundle.LoadAllAssets<Spirte>();
			return altas;
		}
		... ...
	}
	```
从ResourcesAssetProvider中可以看到这里是直接调用了系统的Resources.LoadAll来实现的，比较简单，不多说。而BundleAssetProvider则是先得到了资源集的名字（BundleName），然后又从一个叫bundules里找到了名字叫bundleName的AssetBundle，最后调用AssetBundle.LoadAllAssets实现了资源的获取。这里得到资源集的名字是通过配置表实现的。那么这里的bundles是哪里来的呢，这个就要看下BundleAssetProvider的加载了。BundleAssetProvider的加载过程就是AssetBundle的加载过程，本文的篇幅已经不短了，而且AssetBundle的加载也不是本文的重点，又想了解的童鞋可以到网上找相关的文章，也可以参看本文附录中给出的完整源代码。

### 附 完整源代码

```
// SpriteFactoy.cs
// Author: Iann
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Frame;
using UnityEngine;
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

```

```
// AssetManager.cs
// Autoh: Iann

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Frame;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Threading;
using System.Collections;

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

```
