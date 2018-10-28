using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.Frame;
public class ExportBundles
{
    /// <summary>  
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包  
    /// 之前说过，只要设置了AssetBundleName的，都会进行打包，不论在什么目录下  
    /// </summary>  
    [MenuItem("Tools/BundleOperations/ClearAssetBundlesName")]
    static void ClearAssetBundlesName()
    {
        Debug.Log("ClearAssetBundlesName");
        string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
        foreach (string bundleName in bundleNames)
        {
            AssetDatabase.RemoveAssetBundleName(bundleName, true);
        }
        
        int length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(string.Format("{0} bundles left.", length));
    }

    [MenuItem("Tools/BundleOperations/FillAssetBundleNames")]
    // Use this for initialization
    public static void FillAssetBundleNames()
    {

        EditorUtility.DisplayProgressBar("设置AssetName名称", "正在设置AssetName名称中...", 0f);
        Database.me.Open();
        BundleAssetConfig.me.Load();
        Database.me.Close();
        BundleAssetConfigTableData[] path2names = BundleAssetConfig.me.All();
        foreach (BundleAssetConfigTableData it in path2names)
        {
            Debug.Log(string.Format("BundleName:{0}, AssetPath:{1}", it.BundleName, it.AssetPath));
            AssetImporter assetImporter = AssetImporter.GetAtPath(it.AssetPath);
            assetImporter.assetBundleName = it.BundleName;
        }

        //AssetImporter assetImporter = AssetImporter.GetAtPath("Assets/test.mat");
        //assetImporter.assetBundleName = "abc";
        EditorUtility.ClearProgressBar();

        //string bundlesPath = Application.dataPath + "/../../Bundles";
        //Debug.Log(bundlesPath);
        //Debug.Log(EditorApplication.applicationContentsPath);
        //Debug.Log(EditorApplication.applicationPath);
        //Debug.Log(Application.dataPath);
        //Debug.Log("AssetBundle");
        //BuildPipeline.BuildAssetBundles(bundlesPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Tools/ExcelToSqlite")]
    // Use this for initialization
    public static void ExcelToSqlite()
    {
        string assetPath = Application.dataPath;
        string trunkPath = assetPath + "/../../";
        string pythonName = trunkPath + "Tools/Excel2Sqlite.py";
        string param = pythonName;
        Debug.Log(param);
        System.Diagnostics.Process exep = System.Diagnostics.Process.Start("python", param);
        exep.WaitForExit();//关键，等待外部程序退出后才能往下执行   
       // Debug.Log("启动Server");
    }
}
