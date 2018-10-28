using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Threading;
public class AssetBundleEditor
{

    [MenuItem("Tools/ExportBundles")]
    // Use this for initialization
    public static void ExportBundles()
    {
        string bundlesPath = Application.dataPath + "/../../Bundles";
        Debug.Log(bundlesPath);
        Debug.Log(EditorApplication.applicationContentsPath);
        Debug.Log(EditorApplication.applicationPath);
        Debug.Log(Application.dataPath);
        Debug.Log("AssetBundle");
        BuildPipeline.BuildAssetBundles(bundlesPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }


    [MenuItem("Tools/MakeAltas")]
    // Use this for initialization
    public static void MakeAltas()
    {
        string assetPath = Application.dataPath;
        string trunkPath = assetPath + "/../../";
        string pythonName = trunkPath + "Tools/MakeAltas.py";
        string param = pythonName + " " + trunkPath;
        Debug.Log(param);
        System.Diagnostics.Process exep = System.Diagnostics.Process.Start("python", param);
        exep.WaitForExit();//关键，等待外部程序退出后才能往下执行   
        Debug.Log("導入圖集已經完成");
    }

    [MenuItem("Tools/BootServer")]
    // Use this for initialization
    public static void BootServer()
    {
        string assetPath = Application.dataPath;
        string trunkPath = assetPath + "/../../";
        string pythonName = trunkPath + "Tools/BootServer.py";
        string param = pythonName;
        Debug.Log(param);
        System.Diagnostics.Process exep = System.Diagnostics.Process.Start("python", param);
        exep.WaitForExit();//关键，等待外部程序退出后才能往下执行   
        Debug.Log("启动Server");
    }

    [MenuItem("Tools/Build")]
    // Use this for initialization
    public static void Build()
    {
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "C:/iann1978.NewFrame/trunk/Bin/Client/Client.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        //BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "D:/bbb.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);

    }

    [MenuItem("Tools/Test")]
    // Use this for initialization
    public static void Test()
    {
        WWW www = new WWW("file:///E:/Bundles");
        while (!www.isDone)
        {
            Thread.Sleep(50);
        }
        Debug.Log(www.error);
    }

}
