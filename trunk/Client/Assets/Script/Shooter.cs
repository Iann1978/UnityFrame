using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    // 抛射物
    public GameObject   projectile;
    public Transform    projSpawnAnchor;
    public float        projForce;


    public void Fire()
    {
        Transform spawnTrans = projSpawnAnchor != null ? projSpawnAnchor : transform;
        GameObject goProj = (GameObject)Instantiate(projectile, spawnTrans.position, spawnTrans.rotation);
        Bullet bullet = goProj.GetComponent<Bullet>();
        bullet.Fire(projForce);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }


}
