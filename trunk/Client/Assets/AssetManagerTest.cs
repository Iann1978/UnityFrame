using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
public class AssetManagerTest : MonoBehaviour {


    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,100), "GetSprite"))
        {
            Sprite sp = SpriteFactory.me.Get("HeroHead", 1);
            Debug.Log(sp);
        }
    }
}
