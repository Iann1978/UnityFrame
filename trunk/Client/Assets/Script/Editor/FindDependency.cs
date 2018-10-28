using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Threading;
public class FindDependencyEditor
{

    [MenuItem("Tools/FindDependencyInPrefab")]
    // Use this for initialization
    public static void FindDependencyInPrefab()
    {
        if (Selection.assetGUIDs.Length<1)
        {
            Debug.Log("No Asset Selectted!!!");
            return;
        }

        string selAssetId = Selection.assetGUIDs[0];
        Debug.Log("Selection: " + AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]));

        string[] guids = AssetDatabase.FindAssets("t:prefab");
        foreach(string gid in guids)
        {
            string assetPathname = AssetDatabase.GUIDToAssetPath(gid);
            //Debug.Log(AssetDatabase.GUIDToAssetPath(gid));
            string[] deps = AssetDatabase.GetDependencies(assetPathname);
            foreach(string dep in deps)
            {
                //Debug.Log(dep);
                string depguid = AssetDatabase.AssetPathToGUID(dep);
                if (depguid == selAssetId)
                {
                    Debug.Log(assetPathname + " : " + dep);
                }
            }
        }
        

    }
    
}
